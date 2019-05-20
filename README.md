# Black Jack

This is a simple version of Black Jack played through the Windows Console. 

```
![screenshot](https://github.com/BlackMoonRider/BlackJack/blob/master/screen.jpg)
```

## Features

- You play against Computer
- Computer makes probabilistic decisions based on the hand it holds
- You can't see Computer's cards until it decides that's enough
- The cards are evaluated as they are in a real game: Ace counts as 1 or 11 to form the best hand
- When you get 21 points, you can't ask for another card and ruin your victory
- The game keeps track of players' scores, number of rounds played, and ties
- You can restart the game to reset the statistics 
- When it's a tie, both Player and Computer get 1 point each; when you both go bust, nobody gets points
- It is possible to get no more than 3 extra cards
- The deck of cards gets updated each time there are less than 11 cards left

## Known Issues

- When you've decided to stop getting more extra cards, the list of options should contain only one of them: to press ENTER to show the next turn
