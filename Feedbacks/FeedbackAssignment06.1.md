# Feedback assignment 06.1

Good work, it more or less works. I have encountered similar problems with other participants of this course. I usually get homework with either
1. compile error
2. runtime error
3. logical error

You had runtime error (I had to fix the Program.cs file to launch the web api - I believe that we have the same setting and C# version and everything, so you should have encountered the same problem) and logical errors (the web api does not behave as it should - Delete and Update HTTP requests).
I still consider the homework done, so do not fear :wink:
But we all will have to work on this issue, I will create some guide to our Discord group chat how to check if everything works fine.

Otherwise I would not use the Exceptions in Delete and Update methods in ToDoItemsRepository, but it is a way how to achieve our goals, so why not use it. But beware, when you are using exceptions you need to correctly catch these exceptions. And sometimes it is complicated way how to achieve something (in programming the goal is clear but the path is not set in stone and there are multiple ways how to achieve the same behavior).
