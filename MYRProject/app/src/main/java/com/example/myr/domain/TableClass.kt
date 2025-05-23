package com.example.myr.domain

data class TableResponse(
    val `$id`: String,
    val `$values`: List<TableItem>
)

data class TableItem(
    val `$id`: String,
    val ticketId: Int,
    val tableNumber: Int,
    val date: String,
    val meals: MealWrapper,
    val totalPrice: Double
)

data class MealWrapper(
    val `$id`: String,
    val `$values`: List<Meal>
)

data class Meal(
    val `$id`: String,
    val mealId: Int,
    val name: String,
    val quantity: Int,
    val done: Boolean
)

data class Category(
    val id: Int,
    val name: String
)

data class TableData(
    val tableNumber: Int,
    val orders: List<OrderData>,
    val ticket: Int,
    val totalPrice: Double
)