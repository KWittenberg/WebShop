namespace WebShop.Extensions;

public enum NotificationType
{
    Success,
    Error,
    Info
}

public enum NotificationPosition
{
    Top,
    TopStart,
    TopEnd,
    Center,
    CenterStart,
    CenterEnd,
    Bottom,
    BottomStart,
    BottomEnd
}

public class BaseController : Controller
{
    string pos = "";

    public void BasicNotification(string msj, NotificationType type, string title = "")
    {
        TempData["notification"] = $"Swal.fire('{title}','{msj}', '{type.ToString().ToLower()}')";
    }


    public void DeleteNotification()
    {
        //TempData["notification"] = $"Swal.fire('{title}','{msj}', '{type.ToString().ToLower()}')";

        TempData["notification"] = $"Swal.fire({{\r\n  title: 'Are you sure?',\r\n  text: \"You won't be able to revert this!\",\r\n  icon: 'warning',\r\n  showCancelButton: true,\r\n  confirmButtonColor: '#3085d6',\r\n  cancelButtonColor: '#d33',\r\n  confirmButtonText: 'Yes, delete it!'\r\n}})";
                                   









    }




    // the timer parameter with value 0 is disabled
    public void CustomNotification(string msj, NotificationType type, NotificationPosition position, string title = "", bool showConfirmButton = false, int timer = 2000, bool toast = true)
    {
        SetPosition(position.ToString());

        TempData["notification"] = "Swal.fire({customClass:{confirmButton:'btn btn-primary',cancelButton:'btn btn-danger'},position:'" + pos + "',type:'" + type.ToString().ToLower() +
            "',title:'" + title + "',text: '" + msj + "',showConfirmButton: " + showConfirmButton.ToString().ToLower() + ",confirmButtonColor: '#4F0DA2',toast: "
            + toast.ToString().ToLower() + ",timer: " + timer + "}); ";
    }


    #region Methods

    private void SetPosition(string position)
    {
        if (position == "Top") pos = "top";
        if (position == "TopStart") pos = "top-start";
        if (position == "TopEnd") pos = "top-end";
        if (position == "Center") pos = "center";
        if (position == "CenterStart") pos = "center-start";
        if (position == "CenterEnd") pos = "center-end";
        if (position == "Bottom") pos = "bottom";
        if (position == "BottomStart") pos = "bottom-start";
        if (position == "BottomEnd") pos = "bottom-end";
    }


    #endregion
}