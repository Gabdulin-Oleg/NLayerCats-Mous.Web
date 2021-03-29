using System;
using System.Collections.Generic;
using System.Text;

namespace NLayerCats_Mous.BLL.Hellper
{
    // Статусы Заказов 
    enum EnumOrderStatus 
    {
        RegisteredButNotPaid = 0,
        SuccessfullyPaid = 2,
        AuthorizationCanceled,
        OrderIsBeingProcessed = 7

    }
}
