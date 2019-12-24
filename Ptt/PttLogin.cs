using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace PttLoginner
{
    class PttLogin
    {
        public string user { get; set; }
        public string pass { get; set; }
        const string ptt = "ptt.cc";
        public void getPtt()
        {
            TcpClient tcp = new TcpClient(ptt, 443);
            Byte[] data = new Byte[1024];
            string getMsg = "";
            NetworkStream stream = tcp.GetStream();

            do
            {
                int getByt = stream.Read(data, 0, data.Length);
                getMsg += Encoding.GetEncoding("Big5").GetString(data, 0, getByt);
            } while (stream.DataAvailable);


            Console.WriteLine(getMsg);
        }
        
        public void loginN()
        {
            try
            {
                TcpClient tcp = new TcpClient(ptt, 443);
                Byte[] data = new Byte[1024];
                string getMsg = "";
                NetworkStream stream = tcp.GetStream();
                
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                bool isOut = true;
                bool islog = false;

                while (isOut)
                {
                    getMsg = "";
                    data = new Byte[1024];
                    do
                    {
                        int getByt = stream.Read(data, 0, data.Length);
                        getMsg += Encoding.GetEncoding("Big5").GetString(data, 0, getByt);
                    } while (stream.DataAvailable);

                    Console.WriteLine(getMsg);


                    if (islog)
                    {
                        

                        if (getMsg.IndexOf("您確定要離開")!= -1)
                        {
                            //您確定要離開
                            System.Threading.Thread.Sleep(66);
                            Console.WriteLine("=========================");

                            stream = tcp.GetStream();
                            data = null;
                            data = Encoding.GetEncoding("Big5").GetBytes("Y\r");
                            stream.Write(data, 0, data.Length);

                            Console.WriteLine("=========================");
                            isOut = false;

                            getMsg = "";
                            data = new Byte[1024];
                            do
                            {
                                int getByt = stream.Read(data, 0, data.Length);
                                getMsg += Encoding.GetEncoding("Big5").GetString(data, 0, getByt);
                            } while (stream.DataAvailable);

                            Console.WriteLine(getMsg);
                        }
                        else
                        {
                            Console.WriteLine("=========================");
                            System.Threading.Thread.Sleep(66);
                            stream = tcp.GetStream();
                            data = null;
                            data = Encoding.GetEncoding("Big5").GetBytes("G\r");
                            stream.Write(data, 0, data.Length);

                            Console.WriteLine("=========================");
                        }
                    }


                    if (getMsg.IndexOf("請輸入代號") != -1)
                    {
                        System.Threading.Thread.Sleep(666);

                        Console.WriteLine("=========================");
                        stream = tcp.GetStream();
                        data = null;
                        data = Encoding.GetEncoding("Big5").GetBytes(user + "\r");
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine("send User");
                        Console.WriteLine("=========================");
                    }
                    else if(getMsg.IndexOf("請輸入您的密碼") != -1)
                    {
                        System.Threading.Thread.Sleep(666);

                        Console.WriteLine("=========================");
                        stream = tcp.GetStream();
                        data = null;
                        data = Encoding.GetEncoding("Big5").GetBytes(pass + "\r");
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine("send Pass");
                        Console.WriteLine("=========================");
                    }
                    else if(getMsg.IndexOf("您想刪除") != -1)
                    {
                        System.Threading.Thread.Sleep(50);

                        Console.WriteLine("=========================");
                        stream = tcp.GetStream();
                        data = null;
                        data = Encoding.GetEncoding("Big5").GetBytes("Y" + "\r");
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine("刪除重複連線");
                        Console.WriteLine("=========================");
                    }else if (getMsg.IndexOf("按任意鍵繼續") != -1)
                    {
                        System.Threading.Thread.Sleep(55);

                        Console.WriteLine("=========================");
                        stream = tcp.GetStream();
                        data = null;
                        data = Encoding.GetEncoding("Big5").GetBytes("\r\n");
                        stream.Write(data, 0, data.Length);
                        Console.WriteLine("按任意鍵繼續");
                        Console.WriteLine("=========================");
                        //isOut = false;
                        islog = true;
                    }

                    

                }



                //getMsg = "";
                //data = new Byte[1024];
                //do
                //{
                //    int getByt = stream.Read(data, 0, data.Length);
                //    getMsg += Encoding.GetEncoding("Big5").GetString(data, 0, getByt);
                //} while (stream.DataAvailable);

                //Console.WriteLine(getMsg);

                stream.Close();
                tcp.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
