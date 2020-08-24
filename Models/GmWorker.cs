using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HondaALL.Models.Dto;
using Newtonsoft.Json;
using RestSharp;

namespace HondaALL.Models
{
    public class GmWorker
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
            dict.Add("country", "carea");
            dict.Add("year", "dmodyr");

            return dict;
        }
        public static List<CarTypeInfo> ConversionCarType(List<CarTypeInfoGM> listGM)
        {
            List<CarTypeInfo> list = new List<CarTypeInfo>();

            for (int i = 0; i < listGM.Count; i++)
            {
                CarTypeInfo carType = new CarTypeInfo
                {
                    vehicle_id = listGM[i].vehicle_id,
                    model_name = listGM[i].model_name,
                    country = listGM[i].carea,
                    door = listGM[i].xcardrs,
                    engine = "",
                    trans = listGM[i].ctrsmtyp,
                    year = listGM[i].dmodyr
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
                string strUrl = Ut.GetUrlGM();
                string strParams = GetUrl(param);

                string content = RestWork.GetContent($"{strUrl}/filter-cars?model_id={model_id}{strParams}&page=1&page_size=100000", Method.GET);
                FilterCarsResultGM filterCars = JsonConvert.DeserializeObject<FilterCarsResultGM>(content);

                list = ConversionCarType(filterCars.items);
            }
            catch (Exception ex)
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
                string strUrl = Ut.GetUrlGM();
                string urlParam = GetUrl(param);
                string urla = $"{strUrl}/filters?model_id={model_id}{urlParam}";

                string content = RestWork.GetContent(urla, Method.GET);
                List<Filters> listTmpEU = JsonConvert.DeserializeObject<List<Filters>>(content);
                Dictionary<string, string> dict = GetDictonary();

                for (int i = 0; i < listTmpEU.Count; i++)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        foreach (KeyValuePair<string, string> keyValue in dict)
                        {
                            if ((list[j].filter_id == keyValue.Key) && (listTmpEU[i].filter_id == keyValue.Value))
                            {
                                for (int k = 0; k < listTmpEU[i].values.Count; k++)
                                {
                                    values valuesTMP = new values { name = listTmpEU[i].values[k].name, filter_item_id = $"{keyValue.Key}_{listTmpEU[i].values[k].name}" };

                                    if(!list[j].values.Contains(valuesTMP))
                                    {
                                        list[j].values.Add(valuesTMP);
                                    }
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

            return list;
        }
    }
}
