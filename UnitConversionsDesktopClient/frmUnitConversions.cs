using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnitConversionsDesktopClient.Models;

namespace UnitConversionsDesktopClient
{
    public partial class frmUnitConversions : Form
    {
        public frmUnitConversions()
        {
            InitializeComponent();
            tsslStatus.Text = "Connecting to Microservice...";
        }

        private void frmUnitConversions_Load(object sender, EventArgs e)
        {
            GetConversionTypes();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            double fromValue;
            if(Double.TryParse(txtFromValue.Text, out fromValue))
            {
                ConvertValue(fromValue, cmbConversionTypes.SelectedIndex);
            }
        }

        private async void GetConversionTypes()
        {
            try
            {
                string conversionTypeListJason = string.Empty;

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("http://localhost:60937/api/unitconversions/gettypes");
                    conversionTypeListJason = await httpResponseMessage.Content.ReadAsStringAsync();
                }

                List<ConversionTypeInfo> conversionTypeList = JsonConvert.DeserializeObject<List<ConversionTypeInfo>>(conversionTypeListJason);
                foreach (ConversionTypeInfo conversionTypeInfo in conversionTypeList)
                {
                    cmbConversionTypes.Items.Add(conversionTypeInfo.ConversionName);
                }
                btnConvert.Enabled = true;
                tsslStatus.Text = "Connected successfully to Microservice.";
            }
            catch (HttpRequestException ex)
            {
                btnConvert.Enabled = false;
                tsslStatus.Text = "Failed to connect to Microservice.";
                MessageBox.Show("The Microservice is not responding. Please, make sure it is running and working properly.");
            }        
        }

        private async void ConvertValue(double fromValue, int conversionTypeCode)
        {
            try
            {
                double? convertedValue = null;
                ConversionData conversionData = new ConversionData { ValueFrom = fromValue, ConversionType = (byte)conversionTypeCode };

                using (HttpClient httpClient = new HttpClient())
                {
                    string postContent = JsonConvert.SerializeObject(conversionData);
                    StringContent stringContent = new StringContent(postContent, Encoding.UTF8, "application/json");
                    stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync("http://localhost:60937/api/unitconversions/convert", stringContent);
                    double responseValue = 0;
                    if (Double.TryParse(await httpResponseMessage.Content.ReadAsStringAsync(), out responseValue))
                    {
                        convertedValue = responseValue;
                    }
                }

                txtConvertedValue.Text = convertedValue.ToString();
            }
            catch (HttpRequestException ex)
            {
                btnConvert.Enabled = false;
                MessageBox.Show("The Microservice is not responding. Please, make sure it is running and working properly.");
            }
        }
    }
}
