using System;
using System.Collections.Generic;

namespace cb_ejercicio2.Models;

public partial class CbtProcesodeanalisisCarga
{
    public int? ProcesoDeAnalisisId { get; set; }

    public int? ProcesoId { get; set; }

    public virtual CbtProcesodecarga? Proceso { get; set; }

    public virtual CbtProcesodeanalisi? ProcesoDeAnalisis { get; set; }
}
