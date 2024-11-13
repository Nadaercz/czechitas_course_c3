# Feedback assignment 07.1

It is a pity that only wrote tests for Delete, ReadAll and Create methods.
Be careful with naming. Inconsisten file - class naming can only decrease the clarity of organization of your code (When i want to look up class XY, I will look for XY.cs file).

Do not be afraid to use debuging :wink:

The tests could be more robust, I could find many ways how to deceive some of your tests, but I understand that the main goal was to practise unit testing, not to create perfectly robust tests.

Please follow the "rule" that we use file-scoped namespace. It is never a good idea to have two ways in your code how to do one thing. This can be applied to everything - i.e. when I can use A or B to do one thing, I should always use A or B not both.
