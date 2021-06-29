# AzureFuntionCalculator

Azure function to calculate a result from a set of instructions:  
  * Instructions comprise a keyword and a number that are separated by a space per line.   
  * Instructions are loaded from file and results are output to the screen.  
  * Any number of Instructions can be specified.  
  * Instructions should be the add, divide, multiply and subtract operators, ignoring mathematical precedence.   
  * The last instruction should be "apply" and a number (e.g., "apply"). The calculator is then initialised with that number and the previous instructions are applied to that  number.  

Two examples of the calculator lifecycle might be:  

Example 1.  
[Input from file]  
add 2  
multiply 3  
apply 3  
[Output to screen]  
15  
[Explanation]  
(3 + 2) * 3 = 15  

Example 2.  
[Input from file]  
multiply 9  
apply 5  
[Output to screen]  
45  
[Explanation]  
5 * 9 = 45  
