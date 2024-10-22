# Feedback assignment 04.1

Great work, I can see that this is not the first time you are writing unit tests.

A bit shame that you did not write any test for Update or Create methods :/

I would suggest having separate test files for Read and ReadById methods - they are not the same even though they both have similar functionality.

There is a slight problem with your tests - they are not independent. When you launch single test, all will pass. But when I tried to launch them all at once, all of them failed. But this is most probably due to the static List in controller, so its ok for now, we will be removing it shortly :)
Just be beware of this, I suspect that CI pipeline will scream that some tests are failing when I will create pull request with this feedback.
