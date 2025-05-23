import com.example.myr.domain.CartRequest
import com.example.myr.domain.Category
import com.example.myr.domain.EmployeeClass
import com.example.myr.domain.TableResponse
import com.example.myr.domain.Ticket
import com.example.myr.domain.TicketResponse
import com.example.myr.domain.UpdateOrderDoneRequest
import retrofit2.Call
import retrofit2.Response
import retrofit2.http.Body
import retrofit2.http.DELETE
import retrofit2.http.GET
import retrofit2.http.POST
import retrofit2.http.PUT
import retrofit2.http.Path


interface ApiService {
    @GET("api/Employees/auth/{username}/{password}")
    suspend fun login(
        @Path("username") username: String,
        @Path("password") password: String
    ): Response<EmployeeClass>

    @POST("api/Tickets/createorder")
    suspend fun createNewTicket(
        @Body cartRequest: CartRequest
    ): Response<Void>

    @POST("api/OrderList/updateorder")
    suspend fun updateTicket(
        @Body cartRequest: CartRequest
    ): Response<Void>

    @GET("api/Tickets/restaurant/{restaurantId}/received/full")
    fun getTables(@Path("restaurantId") restaurantId: Int): Call<TableResponse>

    @GET("api/Tickets/{id}")
    fun getTicket(@Path("id") ticketId: Int): Call<TicketResponse>

    @PUT("api/Tickets/{restaurantId}/table/{tableNumber}/markserved")
    fun putMarkServed(
        @Path("restaurantId") restaurantId: Int,
        @Path("tableNumber") tableNumber: Int
    ): Call<Response<Void>>

    @DELETE("api/Tickets/{id}")
    fun deleteTicket(
        @Path("id") id: Int,
    ): Call<Response<Void>>

    @GET("api/Tickets/restaurant/{restaurantId}/table/{tableNumber}/received")
    fun getActiveTicket(
        @Path("restaurantId") restaurantId: Int,
        @Path("tableNumber") tableNumber: Int
    ): Call<Ticket>

    @PUT("api/OrderList/updateorderdone")
    suspend fun updateOrderDone(
        @Body request: UpdateOrderDoneRequest
    ): Response<Void>

    @GET("api/Categories/{categoryId}")
    suspend fun getCategoryName(@Path("categoryId") categoryId: Int): Response<Category>


}

