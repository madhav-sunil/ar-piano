# Plan

Create a AR piano assistant to help people play keyboard. we are using Meta quest 3 for this.


1. Read midi data from midi files.
2. Display data from midi files.
3. Display midi bars from 5 seconds so that players can see it before the note is played.
    - This should be in a way that the notes gets closer to a plane as it gets closer to getting played.
    - Use a fixed plane towards with the notes come to and also use a plane for showing the notes as well
4. Now use the size of the target plane to size up the blocks for the notes relative to it -- it should ideally fit just within the plane.
   You can use 88 keys for this.(Remember white keys and black keys have different size, but we can ignore that for now but keep that in mind for later adaptation because, we will be needing that). 
5. Load the midi files and the show it sequentially as a playing indicator. The midi data should be ideally loaded into a data object and then when the user hits play the midi note come from infrom of the plane like it is now(for now we can play it at start and add a button later on).