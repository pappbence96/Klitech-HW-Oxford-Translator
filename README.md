# Oxford Dictionaries Translator
An API wrapper for the Oxford Dictionaries API v1 (https://developer.oxforddictionaries.com/documentation) and a UWP desktop application built on top of the wrapper.

### Oxford Api Wrapper
A C# wrapper of the Oxford Dictionaries API v1. It does not implement the full functionalit of the web API, only the ones I needed for my task.
Features:
* Querying available dictionaries
* Translating words between languages
* Querying word roots (lemmas)
* Thesaurus:
  * Synonyms
  * Antonyms
* Example sentences

Used frameworks/libraries:
* [Flurl](https://flurl.dev/): easier URL building

### Oxford Translator UWP
UWP desktop application that uses the aforementioned wrapper and provides a Windows Store-like GUI for it.
Used frameworks/libraries:
* [Prism](https://prismlibrary.github.io/): MVVM support

The navigation solution was found at https://github.com/PrismLibrary/Prism-Samples-Windows, big thanks to bartlannoeye for providing Prism samples.
