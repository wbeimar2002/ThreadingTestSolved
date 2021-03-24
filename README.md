# Technical Interview Test

Time to complete the test: 45 minutes.

A simple test of your programming language skills and test driven development.

In this exercise you will be implementing a small read through cache that wraps the factory class `ISprocketFactory`.
Imagine that `ISprocketFactory`’s create method is an expensive method that we want to cache the results.

The focus is on core .Net skills, clean code design and test driven development.

0. Make the project compile and execute the tests

1. Make `CanGetSprocket()` pass by implementing the `get(...)` method in `SprocketCache`.

2. Make `SameKeyReturnsSameSprocket()` pass by having `SprocketCache` cache the result for subsequent calls.

3. Implement the test method `DifferentKeyReturnsDifferentSprocket()` by caching sprockets with different keys.

4. Make `DifferentKeyReturnsDifferentSprocket()` pass.

5. Implement the test method `Expiry()` such that it tests that the cache will only return cached sprockets of less than a given age.

6. Make `Expiry()` pass.

7. Implement the test method `IsThreadSafe()` which will test if same object is returned when accessing cache from two concurrent threads.

8. Finish the `SprocketCache` class such that it passes all tests.