## Recipe Application  

  

### Repository Link:  

https://github.com/VCSTDN/prog6221-poe-OratileKodisang.git 

  

### Changes Implemented  

**Dry:** (Do Not Repeat Yourself)  

My lecture feedback states that my application of DRY is good in part two. I continued to use the DRY application in part three. There are no repetitions throughout the code. 

 

**SOLID:** (single responsibility principle, open-closed principle, Liskov substitution principle, interface segregation principle, and dependency inversion principle) 

- My lecture feedback states that the application of SOLID is good in part one, I continued to make use of the SOLID application in my part 3 continuously throughout my code. Each class has only one responsibility/purpose, such as my ingredients class that only has the purpose of getting ingredient information.  

- My classes are extendable without modifying the classes themselves as shown in my Recipe class where the code 

"public double GetTotalCalories { return Ingredients.Sumi => i.Calories * i.Quantiy);)" calculates the amount of calories without altering the class. 

  

- My classes make use of, implement and depend on one another. My PrintUtil class makes use of my Util folder which consists of MenuUtil, PrintUtil and Constants classes. And most of my classes depend on my Constants class. 

### AS PER LECTURE FEEDBACK: 

- My lecture feedback states my implementation of errorhandeling is impressive. I continued to make use of ErrorHandeling as shown in my ExceptionHandeling class as well as my constants class that contains a list of my error messages that I use in other classes. 

- My lecture feedback asks me to improve user-friendliness of the application by including more writelines that will make it easier for users to interact with my interface. I have included more writelines for the user to input recipe names, ingredient information and selecting a food group. 

- My lecture feedback also states that I should make use of constants for unchanging variables. I did so by creating a constants page where I made use of public constant strings for unchanging variables such as error messages or ingredient information messages etc.  

 

This application is a Graphical user interface that allows users to interact with the application by making use of buttons such as the save recipe button and drop-down menus such as the select food group menu 

 

### How to use the application  

1. Get Started 

1. Enter Recipe Name 

 2. Add Ingredients 

 3. Add Steps 

 4. Save Recipe 

 

To View and Manage Recipes 

   1. Select and View Recipe 

   2. Scale Recipe 

   3. Reset Quantities 

To Filter Recipes 

Click Filter Recipes 
