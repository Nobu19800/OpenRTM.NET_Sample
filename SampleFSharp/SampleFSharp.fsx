#I @"C:\Program Files (x86)\SEC\OpenRTM.NET 1.3\bin"
#r "OpenRTM.Core.dll"
#r "OpenRTM.Extension.dll"
#r "OpenRTM.IIOP.dll"
#r "OpenRTM.WCF.dll"
#light
open OpenRTM.Core
open OpenRTM.Extension
open OpenRTM.IIOP
open System
open System.Collections.Generic
open System.Linq
open System.Text

[<Component(Category = "Examples", Name = "SampleFSharp")>]
[<DetailProfile(
        ActivityType = "DataFlowComponent",
        Description = "Sample FSharp Component",
        Language = "F#",
        LanguageType = "Compile",
        MaxInstance = 10,
        Vendor = "Nobu",
        Version = "1.0.0")>]
[<CustomProfile("CreationDate", "2014/1/9")>]
type SampleFSharp() = 
    inherit DataFlowComponent()
    [<OutPort(PortName = "out")>]
    let mutable outport = new OutPort<TimedShort>()
    [<InPort(PortName = "in")>]
    let mutable inport = new InPort<TimedShort>()
    override this.OnExecute(exec_handle) =
            Console.Write("Please Input Number ")
            let inputStr = Console.ReadLine()
            let input = int16 inputStr
            let mutable data = new TimedShort()
            data.Time.SetCurrentTime()
            let tmp = (data.Data = input)

            outport.Write(data)

            let data = inport.Read()
            
            Console.WriteLine("time = {0}", data.Time)
            Console.WriteLine("data = {0}", data.Data)

            ReturnCode_t.RTC_OK
    
let main() =
    let manager = new Manager()
    manager.AddTypes(typeof<CorbaProtocolManager>)
    manager.Activate()
    let comp = manager.CreateComponent<SampleFSharp>()
    Console.WriteLine(comp.GetComponentProfile().Format())
    manager.Run()
do main()