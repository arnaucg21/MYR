package com.example.myr.activities

import QRMenuResponse
import android.Manifest
import android.content.Intent
import android.content.pm.PackageManager
import android.os.Bundle
import android.util.Log
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.camera.core.*
import androidx.camera.lifecycle.ProcessCameraProvider
import androidx.core.app.ActivityCompat
import androidx.core.content.ContextCompat
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.lifecycle.lifecycleScope
import com.example.myr.databinding.ActivityQrscannerBinding
import com.google.gson.Gson
import com.google.mlkit.vision.barcode.BarcodeScanning
import com.google.mlkit.vision.common.InputImage
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext
import java.net.URL
import java.util.concurrent.Executors

class QRScannerActivity : AppCompatActivity() {

    private lateinit var b: ActivityQrscannerBinding
    private val cameraExecutor = Executors.newSingleThreadExecutor()
    private var qrHandled = false

    companion object {
        private const val REQUEST_CODE_PERMISSIONS = 10
        private val REQUIRED_PERMISSIONS = arrayOf(Manifest.permission.CAMERA)
        private const val TAG = "QRScanner"
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()

        b = ActivityQrscannerBinding.inflate(layoutInflater)
        setContentView(b.root)

        ViewCompat.setOnApplyWindowInsetsListener(b.root) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets

        }
        initListeners()

        if (allPermissionsGranted()) {
            startCamera()
        } else {
            ActivityCompat.requestPermissions(this, REQUIRED_PERMISSIONS, REQUEST_CODE_PERMISSIONS)
        }
    }

    private fun allPermissionsGranted() = REQUIRED_PERMISSIONS.all {
        ContextCompat.checkSelfPermission(baseContext, it) == PackageManager.PERMISSION_GRANTED
    }

    private fun startCamera() {
        val cameraProviderFuture = ProcessCameraProvider.getInstance(this)

        cameraProviderFuture.addListener({
            val cameraProvider = cameraProviderFuture.get()

            val preview = Preview.Builder().build().also {
                it.setSurfaceProvider(b.previewView.surfaceProvider)
            }

            val imageAnalyzer = ImageAnalysis.Builder()
                .setBackpressureStrategy(ImageAnalysis.STRATEGY_KEEP_ONLY_LATEST)
                .build()
                .also {
                    it.setAnalyzer(cameraExecutor, QRCodeAnalyzer())
                }

            val cameraSelector = CameraSelector.DEFAULT_BACK_CAMERA

            try {
                cameraProvider.unbindAll()
                cameraProvider.bindToLifecycle(this, cameraSelector, preview, imageAnalyzer)
            } catch (e: Exception) {
                Log.e(TAG, "Error al iniciar la cámara", e)
            }
        }, ContextCompat.getMainExecutor(this))
    }

    private inner class QRCodeAnalyzer : ImageAnalysis.Analyzer {
        private val scanner = BarcodeScanning.getClient()

        @androidx.camera.core.ExperimentalGetImage
        override fun analyze(imageProxy: ImageProxy) {
            val mediaImage = imageProxy.image
            if (mediaImage != null) {
                val image = InputImage.fromMediaImage(mediaImage, imageProxy.imageInfo.rotationDegrees)

                scanner.process(image)
                    .addOnSuccessListener { barcodes ->
                        if (qrHandled) {
                            imageProxy.close()
                            return@addOnSuccessListener
                        }

                        for (barcode in barcodes) {
                            barcode.rawValue?.let { value ->
                                handleQrCode(value)
                            }
                        }
                    }
                    .addOnFailureListener { e ->
                        Log.e(TAG, "Error escaneando QR", e)
                    }
                    .addOnCompleteListener {
                        imageProxy.close()
                    }
            } else {
                imageProxy.close()
            }
        }

        private fun handleQrCode(value: String) {
            if (!value.startsWith("http")) {
                Toast.makeText(this@QRScannerActivity, "El QR no es una URL válida", Toast.LENGTH_SHORT).show()
                Log.w(TAG, "QR no válido: $value")
                return
            }

            qrHandled = true
            lifecycleScope.launch {
                try {
                    val jsonString = withContext(Dispatchers.IO) {
                        URL(value).readText()
                    }

                    Log.d(TAG, "✅ Texto descargado del QR: $jsonString")

                    if (!jsonString.trim().startsWith("{")) {
                        Log.e(TAG, "❌ La respuesta no es un JSON válido")
                        Toast.makeText(this@QRScannerActivity, "El servidor no devolvió un JSON válido", Toast.LENGTH_SHORT).show()
                        qrHandled = false
                        return@launch
                    }

                    val qrMenu = Gson().fromJson(jsonString, QRMenuResponse::class.java)

                    Log.d(TAG, "✅ Objeto QRMenuResponse parseado: $qrMenu")

                    val intent = Intent(this@QRScannerActivity, CardActivity::class.java)
                    intent.putExtra("qr_data", Gson().toJson(qrMenu))
                    startActivity(intent)
                    finish()

                } catch (e: Exception) {
                    Log.e(TAG, "❌ Error procesando JSON del QR", e)
                    Toast.makeText(this@QRScannerActivity, "Error procesando datos del QR", Toast.LENGTH_SHORT).show()
                    qrHandled = false // permitir reintento
                }
            }
        }
    }

    override fun onRequestPermissionsResult(requestCode: Int, permissions: Array<out String>, grantResults: IntArray) {
        super.onRequestPermissionsResult(requestCode, permissions, grantResults)
        if (requestCode == REQUEST_CODE_PERMISSIONS) {
            if (allPermissionsGranted()) {
                startCamera()
            } else {
                Toast.makeText(this, "Permiso de cámara denegado", Toast.LENGTH_SHORT).show()
                finish()
            }
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        cameraExecutor.shutdown()
    }

    private fun initListeners() {
        b.btnBack.setOnClickListener {
            startActivity(Intent(this@QRScannerActivity, StarterActivity::class.java))
            finish()
        }
    }
}
