workflow foreachptest {

   param([string[]]$computers)

   foreach –parallel ($computer in $computers){

    Get-WmiObject –Class Win32_OperatingSystem –PSComputerName $computer

   }

}