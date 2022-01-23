![Create new chip](https://github.com/JuliProg/NAND01GR3B2CZA6/workflows/Create%20new%20chip/badge.svg?event=repository_dispatch)
![ChipUpdate](https://github.com/JuliProg/NAND01GR3B2CZA6/workflows/ChipUpdate/badge.svg)
# Join the development of the project ([list of tasks](https://github.com/users/JuliProg/projects/1))


# NAND01GR3B2CZA6
Implementation of the NAND01GR3B2CZA6 chip for the JuliProg programmer

Dependency injection, DI based on MEF framework is used to connect the chip to the programmer.

<section class = "listing">

# Chip parameters

```c#


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
             
```
# Chip operations

```c#


            //------- Add chip operations    https://github.com/JuliProg/Wiki#command-set----------------------------------------------------

            myChip.Operations("Reset_FFh").
                   Operations("Erase_60h_D0h").
                   Operations("Read_00h_30h").
                   Operations("PageProgram_80h_10h");

```
# Initial Invalid Block (s)

```c#

            
            //------- Select the Initial Invalid Block (s) algorithm    https://github.com/JuliProg/Wiki/wiki/Initiate-Invalid-Block-----------
                
            myChip.InitialInvalidBlock = "InitInvalidBlock_v1";
                
```
# Chip registers (optional)

```c#


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

```
</section>






















