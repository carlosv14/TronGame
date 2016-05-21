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
	Given I read the player 'Player1'
	And I read the movement 'Player1:L'
	When I generate the turn
	Then The result will be
		| player  | direction|
		| Player1 |     L    |

Scenario: Move Player
	Given I read the player 'Player1'
	And I read the movement 'Player1:R'
	When my current position is 0 0 and the movement is performed
	Then My new position will be 1 0

Scenario: Player Tail Colission
	Given the paloma bien grande en mi boca right now
	And In mi culo grandota y venosa
	When It terminar en mi boca
	Then Swallow Forever