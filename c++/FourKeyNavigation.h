/// Simulates the basic navigation of a four key system (e.g. w,a,s,d)
/// Can be used to navigate a screen of objects (buttons) stacked vertically and horizontally
/// Author: Joshua Jex
/// Date: 7/22/2023

#pragma once

#include "CoreMinimal.h"
#include "Blueprint/UserWidget.h"
#include "FourKeyNavigation.generated.h"
#include "Components/Button.h"

/**
 * Simulates the basic navigation of a four key system (e.g. w,a,s,d)
 * Can be used to navigate a screen of objects (buttons) stacked vertically and horizontally
 * UP, DOWN, LEFT, RIGHT
 * Class is made to work with Unreal Engine 5.
 * @note - objects should be in square symmetrical stacks
 * Buttons array would look like: {{B1, B2}, {B3, B4}, {B5}}
 */
UCLASS()
class UFSFREEDOMRUNNER_API UFourKeyNavigation : public UUserWidget
{
	GENERATED_BODY() //Specific for Unreal Engine integration
	
private:

	/**
	 * An array of pointers to Unreal Engine Button Objects
	 */
	TArray<TArray<UButton*>> buttons;

	/**
	 * An array of the FOUR KEYS that simulate the four key navigation.
	 */
	TArray<FKey> movementKeys;

	/**
	 * The current UButton selected
	 */
	UButton* currentButton;

	/**
	 * The current horizontal index of the current button
	 */
	int lateralIndex;

	/**
	 * The current vertical index of the current button
	 */
	int verticalIndex;

	/**
	 * The maximum vertical index possible (i.e. the number of column-stacked buttons)
	 */
	int maxVerticalIndex;

	/**
	 * The maximum lateral index possible (i.e. the number of side by side buttons)
	 */
	int maxLateralIndex;

	/**
	 * Moves the current button to the next button up.
	 * If there is no button above, current button stays the same.
	 * @see class comments for movement details
	 */
	void MoveUp();

	/**
	 * Moves the current button to the next button below.
	 * If there is no button below, current button stays the same.
	 * @see class comments for movement details
	 */
	void MoveDown();

	/**
	 * Moves the current button to the next button left.
	 * If there is no button to the left, current button stays the same.
	 * @see class comments for movement details
	 */
	void MoveLeft();

	/**
	 * Moves the current button to the next button right.
	 * If there is no button to the right, current button stays the same.
	 * @see class comments for movement details
	 */
	void MoveRight();

public:

	/**
	 * A method built specifically for Unreal Engines interface to intialize the key system
	 * with given:
	 * @param buttons - Two dimensional array to simulate the stacked buttons. 
	 * i.e. buttons[0] gets first row of buttons, buttons[0][0] gets first button in first row
	 * @param fourKeys - four keys that will be used for navigation should be in order of: up, down, left, right
	 * Current button is set to the first button of the first row
	 */
	void Initialize(TArray<TArray<UButton*>> buttons, TArray<FKey> fourKeys);

	/**
	 * Returns the current button
	 */
	UButton* GetCurrentButton();

	/**
	 * Sets the current button to the @param newButton given
	 */
	void SetCurrentButton(UButton* newButton);

	/**
	 * Move the current button in the respective direction given by the key input
	 * @param input - one of the four keys that navigate the current button (up, down, left, right)
	 */
	void Move(FKey Input);
};
