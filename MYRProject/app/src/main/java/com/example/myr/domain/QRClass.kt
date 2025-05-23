import com.google.gson.annotations.SerializedName

data class QRMenuResponse(
    val tableNumber: Int,
    val restaurantId: Int,
    val menuType: String,
    val meals: MealListWrapper? // <- nullable para evitar crashes
)

data class MealListWrapper(
    @SerializedName("\$values")
    val values: List<Meal>? // <- nulla ble para evitar crashes
)

data class Meal(
    val id: Int,
    val name: String,
    val categoryId: Int,
    val details: String,
    val price: Double,
    val photo: String,
    val menuTypes: String,
    val bestFood: Boolean,
    val restaurantId: Int
)
