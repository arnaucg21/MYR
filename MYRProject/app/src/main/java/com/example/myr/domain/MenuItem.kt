package com.example.myr.domain

sealed class MenuItem {
    data class Section(val title: String) : MenuItem()
    data class Food(val foodItem: FoodItem, var quantity: Int = 0) : MenuItem()
}
