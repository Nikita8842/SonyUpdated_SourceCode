using AMBOModels.MasterMaintenance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace AMBOWeb.Classes
{
    public static class FCMNotification
    {
        private static string FCMUrl = Convert.ToString(ConfigurationManager.AppSettings["FCMUrl"]);
        private static HttpClient client;

        public static void Send(List<FCMIDListWithRole> DeviceIds, Int64 MessageId, string MessageTitle, string MessageBody, string AttachmentFileName)
        {
            try
            {
                int CountFcm = DeviceIds.Count();
                string[] DevicesIdss = new string[DeviceIds.Count];

                string[] DevicesIdssSFA = new string[DeviceIds.Count];
                string[] DevicesIdssSID = new string[DeviceIds.Count];

                string[] DevicesIdssSFA1 = new string[DeviceIds.Count];
                string[] DevicesIdssSFA2 = new string[DeviceIds.Count];
                string[] DevicesIdssSFA3 = new string[DeviceIds.Count];
                string[] DevicesIdssSFA4 = new string[DeviceIds.Count];
                string[] DevicesIdssSFA5 = new string[DeviceIds.Count];

                string[] DevicesIdssSID1 = new string[DeviceIds.Count];
                string[] DevicesIdssSID2 = new string[DeviceIds.Count];
                string[] DevicesIdssSID3 = new string[DeviceIds.Count];
                string[] DevicesIdssSID4 = new string[DeviceIds.Count];
                string[] DevicesIdssSID5 = new string[DeviceIds.Count];



                for (int i = 0; i < DeviceIds.Count; i++)
                {
                    if (DeviceIds[i].FCMId != null && DeviceIds[i].FCMId != "")
                    {
                        DevicesIdss[i] = DeviceIds[i].FCMId;
                        //  DevicesIdss[i] = DeviceIds[i].RoleName;
                    }
                }




                for (int i = 0; i < DeviceIds.Count; i++)
                {
                    if (DeviceIds[i].FCMId != null && DeviceIds[i].FCMId != "")
                    {
                        if (DeviceIds[i].RoleName == "SFA")
                        {
                            DevicesIdssSFA[i] = DeviceIds[i].FCMId;
                        }
                        else
                        {
                            DevicesIdssSID[i] = DeviceIds[i].FCMId;
                        }
                    }
                }



                int CountFcmSFA = DevicesIdssSFA.Count();
                int CountFcmSID = DevicesIdssSID.Count();



                client = new HttpClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



                //SEND THE NOTIFICATION TO SFA 

                if (CountFcmSFA != 0 && CountFcmSFA >= 1000)
                {
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }
                else
                {
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }


                //1000-2000 SFA

                if (CountFcmSFA != 0 && CountFcmSFA >= 1000)
                {
                    int Index = 0;
                    for (int i = 1000; i < DevicesIdssSFA.Count(); i++)
                    {
                        DevicesIdssSFA1[Index] = DevicesIdssSFA[i];
                        Index++;
                    }

                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA1.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }
                else
                {

                    int Index = 0;
                    for (int i = 1000; i < DevicesIdssSFA.Count(); i++)
                    {
                        DevicesIdssSFA1[Index] = DevicesIdssSFA[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA1
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }


                //2000-3000 SFA

                if (CountFcmSFA != 0 && CountFcmSFA >= 1000)
                {
                    int Index = 0;
                    for (int i = 2000; i < DevicesIdssSFA.Count(); i++)
                    {
                        DevicesIdssSFA2[Index] = DevicesIdssSFA[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA2.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }
                else
                {

                    int Index = 0;
                    for (int i = 2000; i < DevicesIdssSFA.Count(); i++)
                    {
                        DevicesIdssSFA2[Index] = DevicesIdssSFA[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA2
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }



                //3000-4000 SFA

                if (CountFcmSFA != 0 && CountFcmSFA >= 1000)
                {
                    int Index = 0;
                    for (int i = 3000; i < DevicesIdssSFA.Count(); i++)
                    {
                        DevicesIdssSFA3[Index] = DevicesIdssSFA[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA3.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }
                else
                {

                    int Index = 0;
                    for (int i = 3000; i < DevicesIdssSFA.Count(); i++)
                    {
                        DevicesIdssSFA3[Index] = DevicesIdssSFA[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA3
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }


                //4000-5000 SFA

                if (CountFcmSFA != 0 && CountFcmSFA >= 1000)
                {
                    int Index = 0;
                    for (int i = 4000; i < DevicesIdssSFA.Count(); i++)
                    {
                        DevicesIdssSFA4[Index] = DevicesIdssSFA[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA4.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }
                else
                {

                    int Index = 0;
                    for (int i = 4000; i < DevicesIdssSFA.Count(); i++)
                    {
                        DevicesIdssSFA4[Index] = DevicesIdssSFA[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA4
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }




                //
                //5000-6000 SFA

                if (CountFcmSFA != 0 && CountFcmSFA >= 1000)
                {
                    int Index = 0;
                    for (int i = 5000; i < DevicesIdssSFA.Count(); i++)
                    {
                        DevicesIdssSFA5[Index] = DevicesIdssSFA[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA5.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }
                else
                {
                    int Index = 0;
                    for (int i = 5000; i < DevicesIdssSFA.Count(); i++)
                    {
                        DevicesIdssSFA5[Index] = DevicesIdssSFA[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSFAKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSFA5
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSFA = CountFcmSFA - 1000;
                }


                //

                //SEND THE NOTIFICATION TO SID 

                if (CountFcmSID != 0 && CountFcmSID >= 1000)
                {
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }
                else
                {
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData6 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID
                    };

                    HttpResponseMessage response6 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData6), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }


                //1000-2000 SID

                if (CountFcmSID != 0 && CountFcmSID >= 1000)
                {
                    int Index = 0;
                    for (int i = 1000; i < DevicesIdssSID.Count(); i++)
                    {
                        DevicesIdssSID1[Index] = DevicesIdssSID[i];
                        Index++;
                    }

                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID1.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }
                else
                {

                    int Index = 0;
                    for (int i = 1000; i < DevicesIdssSID.Count(); i++)
                    {
                        DevicesIdssSID1[Index] = DevicesIdssSID[i];
                        Index++;
                    }

                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID1
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }


                //2000-3000 SID

                if (CountFcmSID != 0 && CountFcmSID >= 1000)
                {
                    int Index = 0;
                    for (int i = 2000; i < DevicesIdssSID.Count(); i++)
                    {
                        DevicesIdssSID2[Index] = DevicesIdssSID[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID2.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }
                else
                {

                    int Index = 0;
                    for (int i = 2000; i < DevicesIdssSID.Count(); i++)
                    {
                        DevicesIdssSID2[Index] = DevicesIdssSID[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID2
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }



                //3000-4000 SID

                if (CountFcmSID != 0 && CountFcmSID >= 1000)
                {
                    int Index = 0;
                    for (int i = 3000; i < DevicesIdssSID.Count(); i++)
                    {
                        DevicesIdssSID3[Index] = DevicesIdssSID[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID3.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }
                else
                {
                    int Index = 0;
                    for (int i = 3000; i < DevicesIdssSID.Count(); i++)
                    {
                        DevicesIdssSID3[Index] = DevicesIdssSID[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID3
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }


                //4000-5000 SID

                if (CountFcmSID != 0 && CountFcmSID >= 1000)
                {
                    int Index = 0;
                    for (int i = 4000; i < DevicesIdssSID.Count(); i++)
                    {
                        DevicesIdssSID4[Index] = DevicesIdssSID[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID4.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }
                else
                {

                    int Index = 0;
                    for (int i = 4000; i < DevicesIdssSID.Count(); i++)
                    {
                        DevicesIdssSID4[Index] = DevicesIdssSID[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID4
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }




                //
                //5000-6000 SID

                if (CountFcmSID != 0 && CountFcmSID >= 1000)
                {
                    int Index = 0;
                    for (int i = 5000; i < DevicesIdssSID.Count(); i++)
                    {
                        DevicesIdssSID5[Index] = DevicesIdssSID[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID5.Take(1000)
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }
                else
                {

                    int Index = 0;
                    for (int i = 5000; i < DevicesIdssSID.Count(); i++)
                    {
                        DevicesIdssSID5[Index] = DevicesIdssSID[i];
                        Index++;
                    }
                    client = new HttpClient();
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Convert.ToString(ConfigurationManager.AppSettings["FCMSIDKey"]));

                    var postData1 = new
                    {
                        data = new
                        {
                            MessageId = MessageId,
                            MessageTitle = MessageTitle,
                            MessageBody = MessageBody,
                            MessageAttachment = AttachmentFileName == null ? "" : AttachmentFileName
                        },
                        registration_ids = DevicesIdssSID5
                    };

                    HttpResponseMessage response1 = client.PostAsync(FCMUrl,
                        new StringContent(JsonConvert.SerializeObject(postData1), Encoding.UTF8, "application/json")).Result;
                    CountFcmSID = CountFcmSID - 1000;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
