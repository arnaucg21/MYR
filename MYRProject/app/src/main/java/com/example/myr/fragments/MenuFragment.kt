package com.example.myr.fragments

import QRMenuResponse
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.myr.R
import com.example.myr.activities.CardActivity
import com.example.myr.adapters.MenuSectionAdapter
import com.example.myr.domain.ApiClient
import com.example.myr.domain.FoodItem
import com.example.myr.domain.MenuItem
import com.google.gson.Gson
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class MenuFragment : Fragment(), MenuSectionAdapter.OnQuantityChangeListener {

    private lateinit var recyclerView: RecyclerView
    lateinit var adapter: MenuSectionAdapter
    private val items = mutableListOf<MenuItem>()
    private var restaurantId: Int = 0
    private var tableNumber: Int = 0

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?
    ): View = inflater.inflate(R.layout.fragment_menu, container, false)

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        recyclerView = view.findViewById(R.id.rvDailyMenu)
        recyclerView.layoutManager = LinearLayoutManager(requireContext())
        adapter = MenuSectionAdapter(items, this)
        recyclerView.adapter = adapter

        loadMenu()
    }

    private fun loadMenu() {
        items.clear()

        val qrJson = arguments?.getString("qr_data")

        if (qrJson.isNullOrEmpty()) {
            Toast.makeText(requireContext(), "No se recibieron datos del QR", Toast.LENGTH_SHORT).show()
            adapter.notifyDataSetChanged()
            return
        }

        try {
            val qrMenu = Gson().fromJson(qrJson, QRMenuResponse::class.java)
            restaurantId = qrMenu.restaurantId
            tableNumber = qrMenu.tableNumber

            val meals = qrMenu.meals?.values
            if (meals.isNullOrEmpty()) {
                Toast.makeText(requireContext(), "No hay comidas disponibles", Toast.LENGTH_SHORT).show()
                return
            }

            val groupedMeals = meals.groupBy { it.categoryId ?: 0 }

            lifecycleScope.launch {
                groupedMeals.forEach { (categoryId, mealList) ->
                    try {
                        val response = withContext(Dispatchers.IO) {
                            ApiClient.apiService.getCategoryName(categoryId)
                        }

                        val categoryName = if (response.isSuccessful) {
                            response.body()?.name ?: "Categoría $categoryId"
                        } else {
                            "Categoría $categoryId"
                        }

                        items.add(MenuItem.Section(categoryName))

                        mealList.forEach { meal ->
                            val foodItem = FoodItem(
                                foodId = meal.id,
                                restaurantId = meal.restaurantId,
                                categoryId = categoryId,
                                name = meal.name,
                                details = meal.details,
                                image = R.drawable.hamburger_img,
                                price = meal.price
                            )
                            items.add(MenuItem.Food(foodItem))
                        }

                        adapter.notifyDataSetChanged()

                    } catch (e: Exception) {
                        Log.e("MenuFragment", "Error al obtener nombre categoría $categoryId", e)
                        Toast.makeText(requireContext(), "Error cargando categoría", Toast.LENGTH_SHORT).show()
                    }
                }
            }

        } catch (e: Exception) {
            Log.e("MenuFragment", "Error procesando el JSON del QR", e)
            Toast.makeText(requireContext(), "Error procesando el QR", Toast.LENGTH_SHORT).show()
        }
    }

    fun resetQuantities() {
        items.filterIsInstance<MenuItem.Food>().forEach {
            it.quantity = 0
        }
        adapter.notifyDataSetChanged()
    }

    override fun onQuantityChanged(mealId: Int, quantity: Int) {
        (activity as? CardActivity)?.addItemToCart(mealId, quantity)

        val summary = items.filterIsInstance<MenuItem.Food>()
            .filter { it.quantity > 0 }
            .joinToString(", ") { "${it.foodItem.name} x${it.quantity}" }

        val total = items.filterIsInstance<MenuItem.Food>()
            .sumOf { it.quantity * it.foodItem.price }

        val formattedPrice = "%.2f€".format(total)
        (activity as? CardActivity)?.updateFooterCustom(summary, formattedPrice)
    }

    companion object {
        fun newInstance(qrJson: String): MenuFragment {
            val fragment = MenuFragment()
            val args = Bundle()
            args.putString("qr_data", qrJson)
            fragment.arguments = args
            return fragment
        }
    }
}
