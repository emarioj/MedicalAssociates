﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using MetroFramework.Forms;


namespace Medical_Associate
{
    public partial class imageserver : MetroForm
    {
        public imageserver()
        {
            InitializeComponent(); 
            startListening();
        }
        private void startListening()
        {
            ////////////////////////////////////////////

            textBox1.Text += "Server is starting...";
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

            Socket newsock = new Socket(AddressFamily.InterNetwork,
                            SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);
            newsock.Listen(10);
            textBox1.Text += "Waiting for a client...";

            Socket client = newsock.Accept();
            IPEndPoint newclient = (IPEndPoint)client.RemoteEndPoint;
            textBox1.Text += ("Connected with {0} at port {1}",
                            newclient.Address, newclient.Port);

            while (true)
            {
                data = ReceiveVarData(client);
                MemoryStream ms = new MemoryStream(data);
                try
                {
                    Image bmp = Image.FromStream(ms);
                    pictureBox1.Image = bmp;
                }
                catch (ArgumentException e)
                {
                    textBox1.Text += "something broke";
                }


                if (data.Length == 0)
                    newsock.Listen(10);
            }
            //Console.WriteLine("Disconnected from {0}", newclient.Address);
            client.Close();
            newsock.Close();
            /////////////////////////////////////////////

        }
        private static byte[] ReceiveVarData(Socket s)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];

            recv = s.Receive(datasize, 0, 4, 0);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];


            while (total < size)
            {
                recv = s.Receive(data, total, dataleft, 0);
                if (recv == 0)
                {
                    break;
                }
                total += recv;
                dataleft -= recv;
            }
            return data;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void imageserver_Load(object sender, EventArgs e)
        {

        }
    }
}
