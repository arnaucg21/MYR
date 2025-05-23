package com.example.myr.activities

import android.content.Context
import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.lifecycle.lifecycleScope
import com.example.myr.R
import com.example.myr.databinding.ActivityLoginBinding
import com.example.myr.domain.ApiClient
import kotlinx.coroutines.launch


class LoginActivity : AppCompatActivity() {
    private lateinit var b: ActivityLoginBinding

    override fun attachBaseContext(newBase: Context) {
        super.attachBaseContext(com.example.myr.utils.LocaleHelper.applySavedLocale(newBase))
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        b = ActivityLoginBinding.inflate(layoutInflater)
        setContentView(b.root)

        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }

        initListeners()
    }

    private fun initListeners() {
        b.loginBackBtn.setOnClickListener {
            finish()
        }

        b.loginBtn.setOnClickListener {
            val username = b.emailTxt.text.toString()
            val password = b.passTxt.text.toString()

            if (username.isBlank() || password.isBlank()) {
                Toast.makeText(this, getString(R.string.fill_all_fields), Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            lifecycleScope.launch {
                try {
                    val response = ApiClient.apiService.login(username, password)
                    if (response.isSuccessful) {
                        val user = response.body()

                        val restaurantId = user?.restaurantId ?: 0

                        Toast.makeText(this@LoginActivity, getString(R.string.welcome_user, user?.username ?: ""), Toast.LENGTH_SHORT).show()

                        val intent = Intent(this@LoginActivity, TableActivity::class.java)
                        intent.putExtra("restaurantId", restaurantId)
                        startActivity(intent)
                        finish()

                    } else {
                        Toast.makeText(this@LoginActivity, getString(R.string.invalid_credentials), Toast.LENGTH_SHORT).show()
                    }
                } catch (e: Exception) {
                    e.printStackTrace()
                    Toast.makeText(this@LoginActivity, "Error: ${e.message}", Toast.LENGTH_LONG).show()
                }
            }
        }

    }
}
