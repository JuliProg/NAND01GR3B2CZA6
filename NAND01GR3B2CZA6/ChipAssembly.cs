using NAND_Prog;
using System.ComponentModel.Composition;

namespace NAND01GR3B2CZA6
{
    /*
     use the design :

      # region
         <some code> 
      # endregion

    for automatically include <some code> in the READMY.md file in the repository
    */
    public class ChipPrototype_v1 : ChipPrototype
    {
        public int EccBits ;   
    }


    public class ChipAssembly
    {
        [Export("Chip")]
        ChipPrototype myChip = new ChipPrototype_v1();



        #region Chip parameters

        //--------------------Vendor Specific Pin configuration---------------------------

        //  VSP1(38pin) - NC    
        //  VSP2(35pin) - NC
        //  VSP3(20pin) - NC

        ChipAssembly()
        {
            myChip.devManuf = "ST";
            myChip.name = "NAND01GR3B2CZA6";
            myChip.chipID = "20A10015";                                 // device ID 

            myChip.width = Organization.x8;                          // chip width (x8 or x16)
            myChip.bytesPP = 2048;                                   // page size in bytes
            myChip.spareBytesPP = 64;                                // size Spare Area in bytes
            myChip.pagesPB = 64;                                     // the number of pages per block 
            myChip.bloksPLUN = 1024;                                 // number of blocks in CE 
            myChip.LUNs = 1;                                         // the amount of CE in the chip
            myChip.colAdrCycles = 2;                                 // cycles for column addressing
            myChip.rowAdrCycles = 2;                                 // cycles for row addressing 
            myChip.vcc = Vcc.v1_8;                                   // supply voltage
            (myChip as ChipPrototype_v1).EccBits = 1;                // required Ecc bits for each 512 bytes
             
        #endregion


            #region Chip operations

            //------- Add chip operations    https://github.com/JuliProg/Wiki#command-set----------------------------------------------------

            myChip.Operations("Reset_FFh").
                   Operations("Erase_60h_D0h").
                   Operations("Read_00h_30h").
                   Operations("PageProgram_80h_10h");

            #endregion
                

            #region Initial Invalid Block (s)
            
            //------- Select the Initial Invalid Block (s) algorithm    https://github.com/JuliProg/Wiki/wiki/Initiate-Invalid-Block-----------
                
            myChip.InitialInvalidBlock = "InitInvalidBlock_v1";
                
            #endregion
                
                

            #region Chip registers (optional)

            //------- Add chip registers (optional)----------------------------------------------------

            myChip.registers.Add(                   // https://github.com/JuliProg/Wiki/wiki/StatusRegister
                "Status Register").
                Size(1).
                Operations("ReadStatus_70h").
                Interpretation("SR_Interpreted").
                UseAsStatusRegister();



            myChip.registers.Add(                  // https://github.com/JuliProg/Wiki/wiki/ID-Register
                "Id Register").     
                Size(5).
                Operations("ReadId_90h");
            

            myChip.registers.Add(
              "Parameter Page (ONFI parameter)").
              Size(768).
              Operations("ReadParameterPage_ECh");

            #endregion


        }

     
       
    }

}
