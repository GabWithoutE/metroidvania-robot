# unity-decoupling-plug-play-prefab-functionality
This was a previous project I worked on a couple of years ago (2017-2018), that is now dead. It contains a few ideas I tried to solve some issues I ran into developing in Unity. I just kind of felt like revisiting this, as I'm working on another game now, to see what I can take from it.

## This README is In-Progress, as I need to take time to reread what I had here, this is mostly just written based on what I remember

## Problems I Tried to Solve
### Prefab Coupling
*Problem and Solution*
The paradigm, prefabs, Monobehaviours, GameObjects, offered a lot of potential for reusability of components. An issue it presented, though, was that Monobehaviours would often be coupled to other components, so prefab functionality wouldn't actually be plug and play, they typically required other prefabs containing other components to be present. I circumvented this issue by centralizing state per entity in a single component that contained a dictionary, and building a pattern where state and entity reaction are separate. The interface around the dictionary component allowed functionality to be plug and play. 

*Shortcomings*
An obvious issue was that the solution was pretty expensive. Another issue was that it relied on strings to fetch state information from dictionaries. 

### State Triggers
*Problem and Solution*
There were a lot of methods that I wanted to evoke based off of the change of a state that doesn't change very often. For the state manager--described in the previous problem--I added an API for subscribing to changes in state. Listeners could pass methods as callbacks into the list of listeners on a state change event. This was pretty sweet, and further enabled functionality to be siloed.

*Shortcomings*
Since this was built on the Prefab Coupling solution, it goes without speaking that it came with those. But alone, it also had another issue, which is just that callbacks can end up being pretty difficult to follow and track down.
