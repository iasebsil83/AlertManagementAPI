using System;
using Microsoft.AspNetCore.Mvc;

namespace AlertManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AlertController : ControllerBase {


    //private static readonly DateOnly TODAY = DateOnly(2020, 1, 1);



    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    [HttpGet(Name = "GetAlerts")]
    public Alert Get() {
        return new Alert {
            ID        = 0,
            Message   = "msg",
            Status    = STATUS.DRAFT,
            Area      = "ar",
            CreatedAt = null, //TODAY,
        };
    }
}
