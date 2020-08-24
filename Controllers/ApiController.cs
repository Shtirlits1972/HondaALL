using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HondaALL.Models.Dto;
using HondaALL.Models;

namespace HondaALL.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Route("/locales")]
        public IActionResult GetLang()
        {
            List<lang> list = ClassCrud.GetLang();
            return Json(list);
        }

        [Route("/models")]
        public IActionResult GetModels()
        {
            List<ModelCar> list = ClassCrud.GetModelCars();
            return Json(list);
        }

        [Route("/filters")]   //  [FromQuery] and [FromRoute]
        public IActionResult GetFilters(string model_id, [FromQuery(Name = "params[0]")] string p0, [FromQuery(Name = "params[1]")] string p1,
                [FromQuery(Name = "params[2]")] string p2, [FromQuery(Name = "params[3]")] string p3, [FromQuery(Name = "params[4]")] string p4,
                [FromQuery(Name = "params[5]")] string p5, [FromQuery(Name = "params[6]")] string p6)
        {
            #region
                List<string> param = new List<string>();
                param.Add(p0);
                param.Add(p1);
                param.Add(p2);
                param.Add(p3);
                param.Add(p4);
                param.Add(p5);
                param.Add(p6);
            #endregion
            List<Filters> list = ClassCrud.GetFilters(model_id, param);
            return Json(list);
        }


        [Route("/filter-cars")]
        public IActionResult GetListCarTypeInfoFilterCars(string model_id, 
            [FromQuery(Name = "params[0]")] string p0,
            [FromQuery(Name = "params[1]")] string p1,
            [FromQuery(Name = "params[2]")] string p2,
            [FromQuery(Name = "params[3]")] string p3,
            [FromQuery(Name = "params[4]")] string p4,
            [FromQuery(Name = "params[5]")] string p5,
            [FromQuery(Name = "params[6]")] string p6,
              int page = 1, [FromQuery(Name = "per-page")] int page_size = 10)
        {   //   per-page
            #region
            List<string> param = new List<string>();

            if(!String.IsNullOrEmpty(p0))
            {
                param.Add(p0);
            }
            if (!String.IsNullOrEmpty(p1))
            {
                param.Add(p1);
            }
            if (!String.IsNullOrEmpty(p2))
            {
                param.Add(p2);
            }
            if (!String.IsNullOrEmpty(p3))
            {
                param.Add(p3);
            }
            if (!String.IsNullOrEmpty(p4))
            {
                param.Add(p4);
            }
            if (!String.IsNullOrEmpty(p5))
            {
                param.Add(p5);
            }
            if (!String.IsNullOrEmpty(p6))
            {
                param.Add(p6);
            }
            #endregion

            
            List<CarTypeInfo> list = ClassCrud.GetListCarTypeInfoFilterCars(model_id, param);

            int cnt_itm = list.Count;

            list = list.Skip((page - 1) * page_size).Take(page_size).ToList();

            bool engineEmpty = true;
            bool countryEmpty = true;
            bool yearEmpty = true;

            for(int i =0; i<list.Count; i++)
            {
                if(!String.IsNullOrWhiteSpace(list[i].engine))
                {
                    engineEmpty = false;
                }
                if (!String.IsNullOrWhiteSpace(list[i].country))
                {
                    countryEmpty = false;
                }
                if (!String.IsNullOrWhiteSpace(list[i].year))
                {
                    yearEmpty = false;
                }
            }

            List<header> headerList = ClassCrud.GetHeaders(engineEmpty, countryEmpty, yearEmpty);

            var result1 = new
            {
                header = headerList,
                items = list,
                cnt_items = cnt_itm,
                page = page
            };

            return Json(result1);
        }


        [Route("/vehicle/{vehicle_id:required}/mgroups")]
        public IActionResult GetPartsGroups(string vehicle_id)
        {
            string lang = "EN";
            if (!String.IsNullOrEmpty(Request.Headers["lang"].ToString()))
            {
                lang = Request.Headers["lang"].ToString();
            }

            List<PartsGroup> list = ClassCrud.GetPartsGroup(vehicle_id, lang);
            return Json(list);
           // return Redirect("https://pikabu.ru/new");
        }


        [Route("/vehicle/{vehicle_id:required}")]
        public IActionResult GetVehiclePropArr(string vehicle_id)
        {
            try
            {
                VehiclePropArr vehicleProp = ClassCrud.GetVehiclePropArr(vehicle_id);
                return Json(vehicleProp);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
