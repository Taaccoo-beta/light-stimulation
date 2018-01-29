using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerieslizeControlModule
{


    [Serializable]
    class TParameters
    {
        public double lastTime_1;
        public double intervalTime_1;
        public int circle_1;
        public double voltage_1;

        public double lastTime_2;
        public double intervalTime_2;
        public int circle_2;
        public double voltage_2;

        public double lastTime_3;
        public double intervalTime_3;
        public int circle_3;
        public double voltage_3;

        public double lastTime_4;
        public double intervalTime_4;
        public int circle_4;
        public double voltage_4;
    }

    [Serializable]
    class PIDParameters
    {
        public double kp_up_1;
        public double ki_up_1;
        public double kd_up_1;
        public double kp_up_2;
        public double ki_up_2;
        public double kd_up_2;
        public double kp_up_3;
        public double ki_up_3;
        public double kd_up_3;
        public double kp_up_4;
        public double ki_up_4;
        public double kd_up_4;
        public double kp_up_5;
        public double ki_up_5;
        public double kd_up_5;
        public double kp_up_6;
        public double ki_up_6;
        public double kd_up_6;
        public double kp_up_7;
        public double ki_up_7;
        public double kd_up_7;
        public double kp_up_8;
        public double ki_up_8;
        public double kd_up_8;

        public double kp_down_1;
        public double ki_down_1;
        public double kd_down_1;
        public double kp_down_2;
        public double ki_down_2;
        public double kd_down_2;
        public double kp_down_3;
        public double ki_down_3;
        public double kd_down_3;
        public double kp_down_4;
        public double ki_down_4;
        public double kd_down_4;
        public double kp_down_5;
        public double ki_down_5;
        public double kd_down_5;
        public double kp_down_6;
        public double ki_down_6;
        public double kd_down_6;
        public double kp_down_7;
        public double ki_down_7;
        public double kd_down_7;
        public double kp_down_8;
        public double ki_down_8;
        public double kd_down_8;

    }


    


    class CoreSerialize
    {

        private string Path_PID;
        private string Path_TParam;
       
        public CoreSerialize()
        {
           
                Path_PID = @"tempPID.dat";          
                Path_TParam = @"tempTParameters.dat";
           
        }

        public void TParammSerializeNow(TParameters data)
        {
            FileStream fileStream = new FileStream(Path_TParam, FileMode.OpenOrCreate);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(fileStream, data);
            fileStream.Close();
        }

        public TParameters TParamDeSerializeNow()
        {
            TParameters data = new TParameters();
            FileStream fileStream = new FileStream(Path_TParam, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter b = new BinaryFormatter();
            data = b.Deserialize(fileStream) as TParameters;
            fileStream.Close();

            return data;
        }

        public void PIDSerializeNow(PIDParameters data)
        {
            FileStream fileStream = new FileStream(Path_PID, FileMode.OpenOrCreate);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(fileStream, data);
            fileStream.Close();
        }

        public PIDParameters PIDDeSerializeNow()
        {
            PIDParameters data = new PIDParameters();
            FileStream fileStream = new FileStream(Path_PID, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryFormatter b = new BinaryFormatter();
            data = b.Deserialize(fileStream) as PIDParameters;
            fileStream.Close();

            return data;
        }

    }
}
