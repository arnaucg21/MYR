package com.example.myr.activities

import android.content.Context
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.util.Log
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.GridLayoutManager
import com.example.myr.adapters.TableAdapter
import com.example.myr.databinding.ActivityTableBinding
import com.example.myr.domain.*
import com.example.myr.fragments.TableInfoBottomSheet
import com.example.myr.utils.LocaleHelper
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class TableActivity : AppCompatActivity() {

    private lateinit var b: ActivityTableBinding
    private lateinit var tableAdapter: TableAdapter
    private var restaurantId: Int = 0
    private val handler = Handler(Looper.getMainLooper())
    private val refreshInterval = 500L
    private var menuType: String = ""

    private val autoRefreshRunnable = object : Runnable {
        override fun run() {
            fetchTablesFromApi()
            handler.postDelayed(this, refreshInterval)
        }
    }

    override fun attachBaseContext(newBase: Context) {
        super.attachBaseContext(LocaleHelper.applySavedLocale(newBase))
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        b = ActivityTableBinding.inflate(layoutInflater)
        setContentView(b.root)

        menuType = intent.getStringExtra("menuType") ?: ""
        restaurantId = intent.getIntExtra("restaurantId", 0)
        if (restaurantId == 0) {
            Toast.makeText(this, "RestaurantId no recibido", Toast.LENGTH_SHORT).show()
        }

        setupRecyclerView()
        startAutoRefresh()
        initListeners()
    }

    private fun setupRecyclerView() {
        tableAdapter = TableAdapter(emptyList()) { table ->
            val orders = table.orders.map { order ->
                OrderItem(
                    mealId = order.mealId,
                    description = "${order.quantity}x ${order.name}",
                    done = order.done
                )
            }

            val bottomSheet = TableInfoBottomSheet(
                tableName = "Mesa ${table.tableNumber}",
                orders = orders,
                restaurantId = restaurantId,
                ticketId = table.ticket,
                tableNumber = table.tableNumber,
                onDoneStatusChanged = { mealId, done ->
                    tableAdapter.updateOrderDoneState(table.tableNumber, mealId, done)
                }
            )

            bottomSheet.show(supportFragmentManager, "TableInfoBottomSheet")
        }

        b.rvTables.layoutManager = GridLayoutManager(this, 2)
        b.rvTables.adapter = tableAdapter
    }

    private fun startAutoRefresh() {
        handler.removeCallbacks(autoRefreshRunnable)
        handler.post(autoRefreshRunnable)
    }

    fun fetchTablesFromApi() {
        ApiClient.apiService.getTables(restaurantId).enqueue(object : Callback<TableResponse> {
            override fun onResponse(call: Call<TableResponse>, response: Response<TableResponse>) {
                if (!response.isSuccessful) {
                    Log.e("API_TABLES", "Error en la respuesta: ${response.code()}")
                    return
                }

                val tables = response.body()?.`$values` ?: emptyList()
                if (tables.isEmpty()) {
                    tableAdapter.updateData(emptyList())
                    return
                }

                val adaptedTables = mutableListOf<TableData>()
                var processedCount = 0

                tables.forEach { tableItem ->
                    ApiClient.apiService.getTicket(tableItem.ticketId)
                        .enqueue(object : Callback<TicketResponse> {
                            override fun onResponse(call: Call<TicketResponse>, ticketResponse: Response<TicketResponse>) {
                                val ticket = ticketResponse.body()
                                val totalPrice = if (ticket?.menuType?.contains("buffet", ignoreCase = true) == true) {
                                    0.0
                                } else {
                                    ticket?.totalPrice ?: 0.0
                                }

                                val tableData = TableData(
                                    tableNumber = tableItem.tableNumber,
                                    orders = tableItem.meals.`$values`.map { meal ->
                                        OrderData(
                                            mealId = meal.mealId,
                                            quantity = meal.quantity,
                                            name = meal.name,
                                            done = meal.done
                                        )
                                    },
                                    ticket = tableItem.ticketId,
                                    totalPrice = totalPrice
                                )

                                adaptedTables.add(tableData)
                                processedCount++
                                if (processedCount == tables.size) {
                                    tableAdapter.updateData(adaptedTables.sortedBy { it.tableNumber })
                                }
                            }

                            override fun onFailure(call: Call<TicketResponse>, t: Throwable) {
                                Log.e("API_TICKET", "Error al obtener ticket ${tableItem.ticketId}", t)
                                processedCount++
                                if (processedCount == tables.size) {
                                    tableAdapter.updateData(adaptedTables.sortedBy { it.tableNumber })
                                }
                            }
                        })
                }
            }

            override fun onFailure(call: Call<TableResponse>, t: Throwable) {
                Log.e("API_TABLES", "Fallo en la conexión", t)
            }
        })
    }

    private fun initListeners() {
        b.btnBack.setOnClickListener {
            finish()
        }
    }
}
