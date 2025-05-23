package com.example.myr.domain


data class OrderItem(
    val mealId: Int,
    val description: String,
    var done: Boolean
)

class UpdateOrderDoneRequest (
    val ticketId: Int,
    val mealId: Int,
    val done: Boolean
)

data class OrderData(
    val mealId: Int,
    val quantity: Int,
    val name: String,
    val done: Boolean
)
