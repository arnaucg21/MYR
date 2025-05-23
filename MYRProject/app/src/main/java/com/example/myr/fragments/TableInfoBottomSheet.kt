package com.example.myr.fragments

import android.graphics.Color
import android.graphics.Paint
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.text.SpannableString
import android.text.TextPaint
import android.text.method.LinkMovementMethod
import android.text.style.ClickableSpan
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.lifecycle.lifecycleScope
import com.example.myr.databinding.FragmentTableInfoBinding
import com.example.myr.domain.*
import com.google.android.material.bottomsheet.BottomSheetDialogFragment
import kotlinx.coroutines.launch
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class TableInfoBottomSheet(
    private val tableName: String,
    private val orders: List<OrderItem>,
    private val restaurantId: Int,
    private val ticketId: Int,
    private val tableNumber: Int,
    private val onDoneStatusChanged: (mealId: Int, done: Boolean) -> Unit
) : BottomSheetDialogFragment() {

    private lateinit var b: FragmentTableInfoBinding
    private val underlinedStates = mutableMapOf<Int, Boolean>()
    private val handler = Handler(Looper.getMainLooper())

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        b = FragmentTableInfoBinding.inflate(inflater, container, false)
        return b.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        b.txtTitle.text = tableName
        setupOrderTextList()

        b.btnServed.setOnClickListener { markTableAsServed() }
        b.btnRemove.setOnClickListener { deleteTicket() }
    }

    private fun setupOrderTextList() {
        b.orderListContainer.removeAllViews()

        orders.forEachIndexed { index, orderItem ->
            val textView = TextView(requireContext()).apply {
                textSize = 16f
                setPadding(8, 8, 8, 8)
                highlightColor = Color.TRANSPARENT

                val spannable = SpannableString(orderItem.description)
                val clickableSpan = object : ClickableSpan() {
                    override fun onClick(widget: View) {
                        val newDoneState = !(underlinedStates[index] ?: orderItem.done)
                        underlinedStates[index] = newDoneState

                        paintFlags = if (newDoneState)
                            paintFlags or Paint.STRIKE_THRU_TEXT_FLAG
                        else
                            paintFlags and Paint.STRIKE_THRU_TEXT_FLAG.inv()

                        updateOrderDone(orderItem.mealId, newDoneState)
                        onDoneStatusChanged(orderItem.mealId, newDoneState)
                    }

                    override fun updateDrawState(ds: TextPaint) {
                        super.updateDrawState(ds)
                        ds.isUnderlineText = false
                        ds.color = Color.BLACK
                        ds.bgColor = Color.TRANSPARENT
                    }
                }

                spannable.setSpan(clickableSpan, 0, orderItem.description.length, 0)
                text = spannable
                movementMethod = LinkMovementMethod.getInstance()

                if (orderItem.done) {
                    paintFlags = paintFlags or Paint.STRIKE_THRU_TEXT_FLAG
                    underlinedStates[index] = true
                }
            }

            b.orderListContainer.addView(textView)
        }
    }

    private fun updateOrderDone(mealId: Int, done: Boolean) {
        val request = UpdateOrderDoneRequest(ticketId, mealId, done)

        lifecycleScope.launch {
            try {
                val response = ApiClient.apiService.updateOrderDone(request)
                if (!response.isSuccessful) {
                    Toast.makeText(requireContext(), "Error al actualizar el estado", Toast.LENGTH_SHORT).show()
                }
            } catch (e: Exception) {
                Toast.makeText(requireContext(), "Error de red: ${e.message}", Toast.LENGTH_SHORT).show()
            }
        }
    }

    private fun deleteTicket() {
        ApiClient.apiService.deleteTicket(ticketId).enqueue(object : Callback<Response<Void>> {
            override fun onResponse(call: Call<Response<Void>>, response: Response<Response<Void>>) {
                if (response.isSuccessful) {
                    Toast.makeText(requireContext(), "Ticket eliminado con éxito", Toast.LENGTH_SHORT).show()
                    handler.postDelayed({ dismiss() }, 300) // Espera 300ms para que refresque correctamente
                } else {
                    Toast.makeText(requireContext(), "Error al eliminar el ticket", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<Response<Void>>, t: Throwable) {
                Toast.makeText(requireContext(), "Error de red: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }

    private fun markTableAsServed() {
        ApiClient.apiService.putMarkServed(restaurantId, tableNumber).enqueue(object : Callback<Response<Void>> {
            override fun onResponse(call: Call<Response<Void>>, response: Response<Response<Void>>) {
                if (response.isSuccessful) {
                    Toast.makeText(requireContext(), "Mesa servida con éxito", Toast.LENGTH_SHORT).show()
                    handler.postDelayed({ dismiss() }, 300) // Espera 300ms antes de cerrar
                } else {
                    Toast.makeText(requireContext(), "Error al marcar la mesa como servida", Toast.LENGTH_SHORT).show()
                }
            }

            override fun onFailure(call: Call<Response<Void>>, t: Throwable) {
                Toast.makeText(requireContext(), "Error de red: ${t.message}", Toast.LENGTH_SHORT).show()
            }
        })
    }
}
