package com.example.myr.domain

import com.google.gson.annotations.SerializedName


data class Ticket (
    val id: Int,
    val tableNumber: Int,
    val date: String,
    val totalPrice: Double,
    val billed: Boolean,
    val restaurantId: Int
)

data class TicketResponse(
    val tableNumber: Int,
    val restaurantId: Int,
    val menuType: String?,
    val totalPrice: Double,
    @SerializedName("meals") val meals: TicketMealsWrapper
)

data class TicketMealsWrapper(
    @SerializedName("\$values") val values: List<TicketMeal>
)

data class TicketMeal(
    val id: Int,
    val name: String,
    val price: Double,
    val orderLists: TicketOrderListsWrapper
)

data class TicketOrderListsWrapper(
    @SerializedName("\$values") val values: List<TicketOrderItem>
)

data class TicketOrderItem(
    val mealId: Int,
    val quantity: Int
)
