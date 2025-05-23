package com.example.myr.activities

import QRMenuResponse
import android.os.Bundle
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.fragment.app.commit
import androidx.lifecycle.lifecycleScope
import com.example.myr.R
import com.example.myr.fragments.BuffetFragment
import com.example.myr.fragments.MenuFragment
import com.example.myr.domain.ApiClient
import com.example.myr.domain.CartRequest
import com.example.myr.domain.OrderMeal
import com.example.myr.domain.Ticket
import com.google.gson.Gson
import kotlinx.coroutines.launch
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class CardActivity : AppCompatActivity() {

    private lateinit var txtSummary: TextView
    private lateinit var txtPrice: TextView
    private lateinit var btnSend: Button

    private val mealsInCart = mutableListOf<OrderMeal>()
    private var restaurantId: Int = 0
    private var tableNumber: Int = 0

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_card)

        txtSummary = findViewById(R.id.txtSummary)
        txtPrice = findViewById(R.id.txtPrice)
        btnSend = findViewById(R.id.btnSend)
        btnSend.isEnabled = false

        val qrJson = intent.getStringExtra("qr_data")
        if (qrJson.isNullOrEmpty()) {
            Toast.makeText(this, "No se recibieron datos del QR", Toast.LENGTH_SHORT).show()
            return
        }

        val qrMenu = Gson().fromJson(qrJson, QRMenuResponse::class.java)
        restaurantId = qrMenu.restaurantId
        tableNumber = qrMenu.tableNumber

        val fragment = if (qrMenu.menuType == "Buffet") {
            BuffetFragment.newInstance(qrJson)
        } else {
            MenuFragment.newInstance(qrJson)
        }

        supportFragmentManager.commit {
            replace(R.id.fragmentContainer, fragment)
        }

        btnSend.setOnClickListener {
            sendOrder(qrMenu)
        }
    }

    fun cleanFooter() {
        val summary = mealsInCart.joinToString(", ") { "${it.mealId} x${it.quantity}" }
        val total = mealsInCart.sumOf { it.quantity * 0.00 }
        txtSummary.text = if (summary.isBlank()) getString(R.string.CartItems) else summary
        txtPrice.text = "$total€"
    }

    fun updateFooterCustom(summary: String, formattedPrice: String) {
        txtSummary.text = if (summary.isBlank()) getString(R.string.CartItems) else summary
        txtPrice.text = formattedPrice
    }


    fun addItemToCart(mealId: Int, quantity: Int) {
        val existingMeal = mealsInCart.find { it.mealId == mealId }

        if (existingMeal != null) {
            existingMeal.quantity++
        } else {
            mealsInCart.add(OrderMeal(mealId, quantity))
        }

        btnSend.isEnabled = true
    }

    private fun checkIfTableHasTicket(restaurantId: Int, tableNumber: Int, qrMenu: QRMenuResponse) {
        ApiClient.apiService.getActiveTicket(restaurantId, tableNumber).enqueue(object : Callback<Ticket> {
            override fun onResponse(call: Call<Ticket>, response: Response<Ticket>) {
                if (response.isSuccessful) {
                    val ticket = response.body()
                    if (ticket != null) {
                        updateTicket(ticket.id, restaurantId, tableNumber, qrMenu)
                    }
                } else {
                    createNewTicket(restaurantId, tableNumber, qrMenu)
                }
            }

            override fun onFailure(call: Call<Ticket>, t: Throwable) {
                Toast.makeText(this@CardActivity, "Error de red: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }

    private fun updateTicket(ticketId: Int, restaurantId: Int, tableNumber: Int, qrMenu: QRMenuResponse) {
        val cartRequest = CartRequest(
            restaurantId = restaurantId,
            tableNumber = tableNumber,
            ticketId = ticketId,
            meals = mealsInCart,
            menuType = qrMenu.menuType
        )

        lifecycleScope.launch {
            try {
                val response = ApiClient.apiService.updateTicket(cartRequest)
                if (response.isSuccessful) {
                    Toast.makeText(this@CardActivity, "Ticket actualizado correctamente", Toast.LENGTH_SHORT).show()
                    resetOrderUI()
                } else {
                    Toast.makeText(this@CardActivity, "Error al actualizar el ticket", Toast.LENGTH_SHORT).show()
                }
            } catch (e: Exception) {
                Toast.makeText(this@CardActivity, "Error de red al actualizar el ticket", Toast.LENGTH_SHORT).show()
            }
        }
    }

    private fun createNewTicket(restaurantId: Int, tableNumber: Int, qrMenu: QRMenuResponse) {
        val cartRequest = CartRequest(
            restaurantId = restaurantId,
            tableNumber = tableNumber,
            ticketId = null,
            menuType = qrMenu.menuType,
            meals = mealsInCart
        )

        lifecycleScope.launch {
            try {
                val response = ApiClient.apiService.createNewTicket(cartRequest)
                if (response.isSuccessful) {
                    Toast.makeText(this@CardActivity, "Nuevo ticket creado", Toast.LENGTH_SHORT).show()
                    resetOrderUI()
                } else {
                    Toast.makeText(this@CardActivity, "Error al crear el ticket", Toast.LENGTH_SHORT).show()
                }
            } catch (e: Exception) {
                Toast.makeText(this@CardActivity, "Error de red al crear el ticket", Toast.LENGTH_SHORT).show()
            }
        }
    }

    fun sendOrder(qrMenu: QRMenuResponse) {
        if (mealsInCart.isEmpty()) {
            Toast.makeText(this, "El carrito está vacío", Toast.LENGTH_SHORT).show()
            return
        }

        checkIfTableHasTicket(restaurantId, tableNumber, qrMenu)
    }

    fun resetOrderUI() {
        mealsInCart.clear()
        cleanFooter()
        btnSend.isEnabled = false

        val currentFragment = supportFragmentManager.findFragmentById(R.id.fragmentContainer)
        when (currentFragment) {
            is MenuFragment -> currentFragment.resetQuantities()
            is BuffetFragment -> currentFragment.resetQuantities()
        }
    }
}
