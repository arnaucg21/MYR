package com.example.myr.adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.myr.R
import com.example.myr.domain.MenuItem

class MenuSectionAdapter(
    private val items: List<MenuItem>,
    private val listener: OnQuantityChangeListener
) : RecyclerView.Adapter<RecyclerView.ViewHolder>() {

    companion object {
        private const val TYPE_SECTION = 0
        private const val TYPE_FOOD = 1
    }

    interface OnQuantityChangeListener {
        fun onQuantityChanged(mealId: Int, quantity: Int)
    }


    override fun getItemViewType(position: Int): Int {
        return when (items[position]) {
            is MenuItem.Section -> TYPE_SECTION
            is MenuItem.Food -> TYPE_FOOD
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): RecyclerView.ViewHolder {
        return when (viewType) {
            TYPE_SECTION -> {
                val view = LayoutInflater.from(parent.context).inflate(R.layout.item_section_title, parent, false)
                SectionViewHolder(view)
            }
            TYPE_FOOD -> {
                val view = LayoutInflater.from(parent.context).inflate(R.layout.item_food, parent, false)
                FoodViewHolder(view)
            }
            else -> throw IllegalArgumentException("Invalid view type")
        }
    }

    override fun getItemCount() = items.size

    override fun onBindViewHolder(holder: RecyclerView.ViewHolder, position: Int) {
        when (val item = items[position]) {
            is MenuItem.Section -> (holder as SectionViewHolder).bind(item)
            is MenuItem.Food -> (holder as FoodViewHolder).bind(item)
        }
    }

    inner class SectionViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        private val sectionTitle: TextView = view.findViewById(R.id.sectionTitle)

        fun bind(section: MenuItem.Section) {
            sectionTitle.text = section.title
        }
    }

    inner class FoodViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        private val title: TextView = view.findViewById(R.id.txtTitle)
        private val ingredients: TextView = view.findViewById(R.id.txtIngredients)
        private val image: ImageView = view.findViewById(R.id.imgFood)
        private val btnAdd: Button = view.findViewById(R.id.btnAdd)
        private val btnRemove: Button = view.findViewById(R.id.btnRemove)
        private val quantity: TextView = view.findViewById(R.id.txtQuantity)
        private val price: TextView = view.findViewById(R.id.txtPrice)

        fun bind(food: MenuItem.Food) {
            title.text = food.foodItem.name
            ingredients.text = food.foodItem.details
            food.foodItem.image?.let { image.setImageResource(it) }
            quantity.text = "x${food.quantity}"
            price.text = "€${food.foodItem.price}"

            btnAdd.setOnClickListener {
                food.quantity++
                quantity.text = "x${food.quantity}"

                listener.onQuantityChanged(food.foodItem.foodId, food.quantity)
            }

            btnRemove.setOnClickListener {
                if (food.quantity > 0) {
                    food.quantity--
                    quantity.text = "x${food.quantity}"
                    listener.onQuantityChanged(food.foodItem.foodId, food.quantity)
                }
            }
        }
    }

}
