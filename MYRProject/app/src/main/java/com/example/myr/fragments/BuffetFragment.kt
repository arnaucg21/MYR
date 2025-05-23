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

class BuffetFragment : Fragment(), MenuSectionAdapter.OnQuantityChangeListener {

    private lateinit var recyclerView: RecyclerView
    lateinit var adapter: MenuSectionAdapter
    private val items = mutableListOf<MenuItem>()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?
    ): View = inflater.inflate(R.layout.fragment_buffet, container, false)

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        recyclerView = view.findViewById(R.id.rvBuffetMenu)
        recyclerView.layoutManager = LinearLayoutManager(requireContext())
        adapter = MenuSectionAdapter(items, this)
        recyclerView.adapter = adapter

        loadBuffetMenu()
    }

    private fun loadBuffetMenu() {
        items.clear()

        val buffetJson = arguments?.getString("buffet_data")

        if (buffetJson.isNullOrEmpty()) {
            Toast.makeText(requireContext(), "No se recibieron datos del Buffet", Toast.LENGTH_SHORT).show()
            adapter.notifyDataSetChanged()
            return
        }

        try {
            val buffetMenu = Gson().fromJson(buffetJson, QRMenuResponse::class.java)
            val meals = buffetMenu.meals?.values

            if (meals.isNullOrEmpty()) {
                Toast.makeText(requireContext(), "No hay opciones disponibles", Toast.LENGTH_SHORT).show()
                Log.e("BuffetFragment", "meals es null o vacío")
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
                                price = 0.00
                            )
                            items.add(MenuItem.Food(foodItem))
                        }

                        adapter.notifyDataSetChanged()

                    } catch (e: Exception) {
                        Log.e("BuffetFragment", "Error al obtener nombre categoría $categoryId", e)
                        Toast.makeText(requireContext(), "Error cargando categoría", Toast.LENGTH_SHORT).show()
                    }
                }
            }

        } catch (e: Exception) {
            Log.e("BuffetFragment", "Error procesando JSON del Buffet", e)
            Toast.makeText(requireContext(), "Error procesando los datos del Buffet", Toast.LENGTH_SHORT).show()
        }
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

    fun resetQuantities() {
        items.filterIsInstance<MenuItem.Food>().forEach {
            it.quantity = 0
        }
        adapter.notifyDataSetChanged()
    }

    companion object {
        fun newInstance(buffetJson: String): BuffetFragment {
            val fragment = BuffetFragment()
            val args = Bundle()
            args.putString("buffet_data", buffetJson)
            fragment.arguments = args
            return fragment
        }
    }
}
