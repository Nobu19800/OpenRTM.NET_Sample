import clr
import sys

clr.AddReference("OpenRTM.Core")
clr.AddReference("OpenRTM.Extension")
clr.AddReference("OpenRTM.IIOP")
clr.AddReference("OpenRTM.WCF")

from OpenRTM.Core import *
from OpenRTM.Extension import *
from OpenRTM.IIOP import *
from System import *


class SampleIronPython(DataFlowComponent):
    def __init__(self):
        DataFlowComponent.__init__(self)
        self.inport = InPort[TimedShort]()
        self.outport = OutPort[TimedShort]()
    def OnInitialize(self):
        self.RegisterInPort("in", self.inport, TypeKind.DynamicContract)
        self.RegisterOutPort("out", self.outport, TypeKind.DynamicContract)

        return ReturnCode_t.RTC_OK
    
    def OnExecute(self, execHandle):
        print "Please Input Number "
        
        data = TimedShort()
        
        #data.Time.SetCurrentTime()
        data.Data = long(sys.stdin.readline())
        self.outport.Write(data)
        
        data = self.inport.Read()
        if data is None:
            return ReturnCode_t.RTC_OK
        print "time = " + format(data.Time)
        print "data = " + str(data.Data)
        return ReturnCode_t.RTC_OK

def rtc_finalize(rtc):
    rtc.Finalize()
    

def SampleIronPythonInit(manager):
    profile = ComponentProfile()
    profile.InstanceName = "SampleIronPython"
    profile.TypeName = "SampleIronPython"
    profile.Category = "Examples"
    profile.Description = "Sample IronPython Component"
    profile.Vendor = "Nobu"
    profile.Version = "1.0"
    manager.DefaultProtocol.RegisterComponent(
        profile, SampleIronPython, rtc_finalize)
        

def main():
    
    manager = Manager(sys.argv)
    manager.AddTypes(CorbaProtocolManager)
    manager.Activate()
    SampleIronPythonInit(manager)
    comp = manager.CreateComponent("Examples.SampleIronPython")
    #print format(comp.GetComponentProfile())
    manager.Run()


if __name__ == "__main__":
	main()