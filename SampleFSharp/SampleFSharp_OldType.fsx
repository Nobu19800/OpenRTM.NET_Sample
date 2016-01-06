#r "OpenRTM.Core"
#r "OpenRTM.Extension"
#r "OpenRTM.IIOP"
#r "OpenRTM.WCF"

open OpenRTM.Core
open OpenRTM.Extension
open OpenRTM.IIOP
open System
open System.Collections.Generic
open System.Linq
open System.Text

type SampleFSharp_OldType() = 
    inherit DataFlowComponent()

    let mutable inport = new InPort<TimedShort>()
    let mutable outport = new OutPort<TimedShort>()
    let int_value = 0

    override this.OnInitialize() =
            this.RegisterInPort("in", inport, TypeKind.DynamicContract)
            this.RegisterOutPort("out", outport, TypeKind.DynamicContract)
            ReturnCode_t.RTC_OK

    override this.OnExecute(exec_handle) =
            
            Console.Write("Please Input Number ")
            let inputStr = Console.ReadLine()

            if not (inputStr = "") then
                let input = int16 inputStr
                let mutable data = new TimedShort()
                data.Time.SetCurrentTime()
                data.Data <- input
                outport.Write(data)

            
            if inport.IsNew<TimedShort>() then
                let data = inport.Read()
                Console.WriteLine("time = {0}", data.Time.ToDateTime())
                Console.WriteLine("data = {0}", data.Data)
                

            ReturnCode_t.RTC_OK














let RTC_Finalize(rtc:IRTObject): unit = 
    let ret = rtc.Finalize()
    ()




let getComponentProfile() : ComponentProfile = 
    let mutable profile = new ComponentProfile()
    profile.InstanceName <- "SampleFSharp_OldType"
    profile.TypeName <- "SampleFSharp_OldType"
    profile.Category <- "Examples"
    profile.Description <- "Sample FSharp Component OldType"
    profile.Vendor <- "Nobu"
    profile.Version <- "1.0"
    Console.WriteLine(profile.Format())
    profile



let SampleFSharp_OldTypeInit(manager: Manager) = 
    let profile = getComponentProfile()
    manager.DefaultProtocol.RegisterComponent(profile, (fun () -> upcast new SampleFSharp_OldType()),fun (rtc:IRTObject) -> RTC_Finalize(rtc))
    
    
    
let main() =
    let mutable manager = new Manager()
    manager.AddTypes(typeof<CorbaProtocolManager>)
    manager.Activate()
    SampleFSharp_OldTypeInit(manager);
    let mutable comp = manager.CreateComponent("Examples.SampleFSharp_OldType")
    Console.WriteLine(comp.GetComponentProfile().Format())
    manager.Run()
do main()