package com.example.myr.domain

data class OrderMeal(
    val mealId: Int,
    var quantity: Int
)

data class CartRequest(
    val restaurantId: Int,
    val tableNumber: Int,
    val ticketId: Int?,
    val menuType: String,
    val meals: List<OrderMeal>,
)



