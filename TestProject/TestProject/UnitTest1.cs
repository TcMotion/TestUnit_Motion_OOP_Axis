using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwinCAT.Ads;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {

        TwinCAT.Ads.AdsClient client = new AdsClient();

        public enum MC_AxisStates : System.Int16
        {
            MC_AXISSTATE_UNDEFINED,
            MC_AXISSTATE_DISABLED,
            MC_AXISSTATE_STANDSTILL,
            MC_AXISSTATE_ERRORSTOP,
            MC_AXISSTATE_STOPPING,
            MC_AXISSTATE_HOMING,
            MC_AXISSTATE_DISCRETEMOTION,
            MC_AXISSTATE_CONTINOUSMOTION,
            MC_AXISSTATE_SYNCHRONIZEDMOTION
        }

        bool result_power;


        public TestContext TestContext { get; set; }

        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {

        }

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {

        }

        [TestInitialize()]
        public void Initialize()
        {

            client.Connect(851);

            //fails...
            //{attribute ‘monitoring’ := ‘call’}
            //result_power = (byte)client.ReadValue("WareHousingExample.fbAxis_X1.PowerOn", typeof(byte)); //fails...???
            //TestContext.WriteLine(" ", ++result_power);
            
            //fails...
            //string varName = "WareHousingExample.fbAxis_X1.PowerOn"; //                  
            //var varHdl = client.CreateVariableHandle(varName);
            //result_power = (bool)client.ReadAny(varHdl, typeof(bool));

        }

        [TestCleanup]
        public virtual void Cleanup()
        {
            client.Close();
        }

        [TestMethod]
        [System.Obsolete]
        public void TestMethod01_MC_Power()
        {

            bool expected = true;
            //byte result = result_power;

            client.InvokeRpcMethod("WareHousingExample.fbAxis_X1", "Power", new object[] { true, true, true });
            //byte result = (byte)client.ReadValue("WareHousingExample.fbAxis_X1.PowerOn", typeof(byte));

            //AdsStream dataStream = new AdsStream(2);
            //var varHdl = client.CreateVariableHandle("WareHousingExample.fbAxis_X1.PowerOn");
            ////bool result = (bool)client.Read(varHdl, dataStream);

            string varName = "WareHousingExample.fbAxis_X1._PowerOn";                 
            var varHdl = client.CreateVariableHandle(varName);
            bool result = (bool)client.ReadAny(varHdl, typeof(bool));

            Assert.AreEqual(expected, result);
            TestContext.WriteLine("MC_Power, se Esperaba: {0}, Resultado de MC_Power es: {1}", expected, result);

        }

        [TestMethod]
        public void TestMethod2_Sum()
        {
            System.Int16 expected = 3;
            System.Int16 result = (System.Int16)client.InvokeRpcMethod("MAIN_Examples.fbSum", "Sum", new object[] { (System.Int16)1, (System.Int16)2 });
            Assert.AreEqual(expected, result);
            TestContext.WriteLine("Sum, se Esperaba: {0}, Resultado de Sum es: {1}", expected, result);
        }
    


    }
}
