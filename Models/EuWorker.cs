using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using HondaALL.Models.Dto;
using Newtonsoft.Json;
using RestSharp;

namespace HondaALL.Models
{
    public class EuWorker
    {
        public static string GetUrl(List<string> param)
        {
            string url = string.Empty;
            Dictionary<string, string> dict = GetDictonary();

            try
            {
                for (int i = 0; i < param.Count; i++)
                {
                    if (!String.IsNullOrWhiteSpace(param[i]))
                    {
                        //  door_3
                        if (param[i].IndexOf("_") > -1)
                        {
                            string p1 = param[i].Substring(0, param[i].IndexOf("_"));
                            string p2 = param[i].Substring(param[i].IndexOf("_") + 1, param[i].Length - (param[i].IndexOf("_") + 1));
                            foreach (KeyValuePair<string, string> keyValue in dict)
                            {
                                if (keyValue.Key == p1)
                                {
                                    url += $"&params[{i}]={keyValue.Value}_{p2}";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string Error = ex.Message;
                int o = 0;
            }
            return url;
        }
        public static Dictionary<string, string> GetDictonary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("door", "xcardrs");
            dict.Add("trans", "ctrsmtyp");
            dict.Add("engine", "nengnpf");
            dict.Add("country", "carea");
            dict.Add("year", "dmodyr");

            return dict;
        }
        public static List<CarTypeInfo> ConversionCarType(List<CarTypeInfoEU> listEU)
        {
            List<CarTypeInfo> list = new List<CarTypeInfo>();

            for (int i = 0; i < listEU.Count; i++)
            {
                CarTypeInfo carType = new CarTypeInfo
                {
                    vehicle_id = listEU[i].vehicle_id,
                    model_name = listEU[i].model_name,
                    country = listEU[i].carea,
                    door = listEU[i].xcardrs,
                    engine = listEU[i].nengnpf,
                    trans = listEU[i].ctrsmtyp,
                    year = listEU[i].dmodyr
                };
                list.Add(carType);
            }

            return list;
        }
        public static List<CarTypeInfo> GetFilterCars(string model_id, List<string> param)
        {
            List<CarTypeInfo> list = new List<CarTypeInfo>();

            try
            {
                string strUrl = Ut.GetUrlEU();
                string strParams = EuWorker.GetUrl(param);

                string UrlEU = $"{strUrl}/filter-cars?model_id={model_id}{strParams}&page=1&page_size=100000";  //   

                string content = RestWork.GetContent(UrlEU, Method.GET);
                FilterCarsResultEU filterCars = JsonConvert.DeserializeObject<FilterCarsResultEU>(content);

                list = ConversionCarType(filterCars.items);

            }
            catch(Exception ex)
            {
                string Error = ex.Message;
                int o = 0;
            }
            return list;
        }
        public static List<Filters> GetFilters(string model_id, List<string> param, List<Filters> list)
        {
            try
            {
                string strUrl = Ut.GetUrlEU();
                string urlParam = GetUrl(param);
                string content = RestWork.GetContent($"{strUrl}/filters?model_id={model_id}{urlParam}", Method.GET);
                List<Filters> listTmpEU = JsonConvert.DeserializeObject<List<Filters>>(content);
                Dictionary<string, string> dictEU = GetDictonary();

                for (int i = 0; i < listTmpEU.Count; i++)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        foreach (KeyValuePair<string, string> keyValue in dictEU)
                        {
                            if ((list[j].filter_id == keyValue.Key) && (listTmpEU[i].filter_id == keyValue.Value))
                            {
                                for (int k = 0; k < listTmpEU[i].values.Count; k++)
                                {
                                    values valuesTMP = new values { name = listTmpEU[i].values[k].name, filter_item_id = $"{keyValue.Key}_{listTmpEU[i].values[k].name}" };
                                    if (!list[j].values.Contains(valuesTMP))
                                    {
                                        list[j].values.Add(valuesTMP);
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                string Error = ex.Message;
                int o = 0;
            }

            return list;
        }

    }
}

