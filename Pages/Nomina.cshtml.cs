using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Desarrollo_web_ASP.NET_Core_Practica_2.Pages
{
    public class NominaModel : PageModel
    {
        public List<Empleado> empleados { get; set; }

        public NominaModel()
        {
            empleados = new List<Empleado>
            {
                new Empleado { id = Guid.NewGuid(), nombre = "Erinxon", apellido = "Santana", cargo = "Programador", salario = 50000},
                new Empleado { id = Guid.NewGuid(), nombre = "Ariel", apellido = "Santana", cargo = "Programador", salario = 60000},
                new Empleado { id = Guid.NewGuid(), nombre = "Danilo", apellido = "Medina", cargo = "Programador", salario = 30000},
                new Empleado { id = Guid.NewGuid(), nombre = "Leonel", apellido = "Fernandez", cargo = "Programador", salario = 40000},
                new Empleado { id = Guid.NewGuid(), nombre = "Luis", apellido = "Abinader", cargo = "Lider de proyectos", salario = 90000},
            };
        }

        public void OnGet()
        {

        }

        private decimal GetSalarioEmpleadoById(Guid id)
        {
            return this.empleados.SingleOrDefault(e => e.id == id).salario;
        }

        public decimal GetDescuentoAFP(Guid id)
        {
            var salarioEmpleado = GetSalarioEmpleadoById(id);
            decimal descuento = (salarioEmpleado * decimal.Parse(2.87.ToString())) / 100;
            return descuento > 7738.67m ? 7738.67m : descuento;
        }

        public decimal GetDescuentoARS(Guid id)
        {
            var salarioEmpleado = GetSalarioEmpleadoById(id);
            decimal descuento = (salarioEmpleado * 3.04m) / 100;
            return descuento > 4098.53m ? 4098.53m : descuento;
        }

        public decimal GetDescuentoISR(Guid id)
        {
            decimal salarioMensual = GetSalarioEmpleadoById(id) - GetDescuentoARS(id) - GetDescuentoAFP(id);

            if (salarioMensual <= 34685.00m)
            {
                return 0;
            }
            else if (salarioMensual > 34685.00m && salarioMensual < 52027.42m)
            {
                return decimal.Round((salarioMensual - 34685.00m)  * 0.15m, 2);
            }
            else if(salarioMensual > 52027.42m && salarioMensual <= 72260.25m)
            {
                return decimal.Round(2601.33m + ((salarioMensual - 52027.42m) * 0.20m), 2);
            }
            else
            {
                return decimal.Round(6648.00m + ((salarioMensual - 72260.25m) * 0.25m), 2);
            }
        }

        public decimal GetTotaldescuentos(Guid id)
        {
            return GetDescuentoAFP(id) + GetDescuentoARS(id) + GetDescuentoISR(id);
        }

        public decimal GetSalarioNeto(Guid id)
        {
            return GetSalarioEmpleadoById(id) - GetTotaldescuentos(id);
        }
    }

    public class Empleado
    {
        public Guid id {get;set;}
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string cargo { get; set; }
        public decimal salario { get; set; }
    }
}
