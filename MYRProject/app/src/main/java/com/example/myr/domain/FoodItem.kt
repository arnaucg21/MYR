package com.example.myr.domain

data class FoodItem(
    val foodId: Int,
    val categoryId: Int,
    val restaurantId: Int,
    val name: String,
    val details: String,
    val image: Int?,
    val price: Double = 0.0
)
