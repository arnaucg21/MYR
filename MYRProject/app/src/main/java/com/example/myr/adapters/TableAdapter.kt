package com.example.myr.adapters

import android.text.SpannableString
import android.text.Spanned
import android.text.style.StrikethroughSpan
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.myr.R
import com.example.myr.domain.TableData


class TableAdapter(
    private var tables: List<TableData>,
    private val onTableSelected: (TableData) -> Unit
) : RecyclerView.Adapter<TableAdapter.TableViewHolder>() {

    private var selectedTableNumber: Int? = null

    inner class TableViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        val tableNumberText: TextView = view.findViewById(R.id.tvTableNumber)
        val ordersText: TextView = view.findViewById(R.id.tvOrders)
        val totalPriceText: TextView = view.findViewById(R.id.tvTotalPrice)

        init {
            view.setOnClickListener {
                val table = tables[adapterPosition]
                selectedTableNumber = table.tableNumber
                notifyDataSetChanged()
                onTableSelected(table)
            }
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): TableViewHolder {
        val view = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_table, parent, false)
        return TableViewHolder(view)
    }

    override fun getItemCount(): Int = tables.size

    override fun onBindViewHolder(holder: TableViewHolder, position: Int) {
        val table = tables[position]
        holder.tableNumberText.text = "Mesa ${table.tableNumber}"
        holder.totalPriceText.text = "%.2f€".format(table.totalPrice)

        if (table.orders.isNotEmpty()) {
            val spannableText = SpannableString(buildString {
                table.orders.forEachIndexed { i, order ->
                    if (i > 0) append("\n")
                    append("${order.quantity}x ${order.name}")
                }
            })

            var currentIndex = 0
            table.orders.forEach { order ->
                val line = "${order.quantity}x ${order.name}"
                if (order.done) {
                    spannableText.setSpan(
                        StrikethroughSpan(),
                        currentIndex,
                        currentIndex + line.length,
                        Spanned.SPAN_EXCLUSIVE_EXCLUSIVE
                    )
                }
                currentIndex += line.length + 1
            }

            holder.ordersText.text = spannableText
        } else {
            holder.ordersText.text = "No hay pedidos"
        }
    }

    fun updateData(newTables: List<TableData>) {
        tables = newTables
        notifyDataSetChanged()
    }

    fun updateOrderDoneState(tableNumber: Int, mealId: Int, done: Boolean) {
        val updated = tables.map { table ->
            if (table.tableNumber == tableNumber) {
                val updatedOrders = table.orders.map { order ->
                    if (order.mealId == mealId) {
                        order.copy(done = done)
                    } else {
                        order
                    }
                }
                table.copy(orders = updatedOrders)
            } else {
                table
            }
        }
        updateData(updated)
    }
}
