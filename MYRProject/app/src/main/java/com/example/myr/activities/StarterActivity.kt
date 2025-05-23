package com.example.myr.activities

import android.content.Context
import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.example.myr.databinding.ActivityStarterBinding
import com.example.myr.utils.LocaleHelper

class StarterActivity : AppCompatActivity() {

    private lateinit var b: ActivityStarterBinding

    override fun attachBaseContext(newBase: Context) {
        super.attachBaseContext(LocaleHelper.applySavedLocale(newBase))
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        b = ActivityStarterBinding.inflate(layoutInflater)
        setContentView(b.root)

        ViewCompat.setOnApplyWindowInsetsListener(b.root) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }

        initListeners()
    }

    private fun initListeners() {
        b.btnWorker.setOnClickListener {
            startActivity(Intent(this, LoginActivity::class.java))
        }

        b.btnClient.setOnClickListener {
            startActivity(Intent(this, QRScannerActivity::class.java))
        }

        b.spaBtn.setOnClickListener {
            changeLanguage("es")
        }

        b.engbtn.setOnClickListener {
            changeLanguage("en")
        }

        b.catBtn.setOnClickListener {
            changeLanguage("ca")
        }
    }

    private fun changeLanguage(languageCode: String) {
        LocaleHelper.setLocale(this, languageCode)
        recreate()
    }
}
