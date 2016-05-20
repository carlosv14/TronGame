Feature: Tron
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Read movement file 
	Given I have a file named 'moves.txt'
	When I read the file
	Then contents will be 'Player1;Player2|Player1:L,Player2:R'

Scenario: Assing movement to correct player
	Given I read the string 'Player1;Player2|Player1:L,Player2:R'
	When I parse the movements
	Then The result will be
		| player  | direction|
		| Player1 |     L    |
		| Player2 |     R    |  

Scenario: Move Player
	Given I have the movement 'Player1:R'
	And My x position is 0
	And My y position is 0
	When the movement is performed
	Then My current position will be
		| x | y |
		| 1 | 0 |