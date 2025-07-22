///Simulates the nagivation of four keys (e.g. w,a,s,d)

#include "FourKeyNavigation.h"

void UFourKeyNavigation::Initialize(TArray<TArray<UButton*>> buttons, TArray<FKey> fourKeys)
{
	this->buttons = buttons;

	this->movementKeys = fourKeys;

	currentButton = buttons[0][0];

	lateralIndex = 0;

	verticalIndex = 0;

	maxVerticalIndex = 0;

	// maxLaterIndex and maxVerticalIndex are calculated automatically from @buttons
	maxLateralIndex = buttons.Num() - 1;

	// Get maxLateralIndex by comparing all row sizes
	for (int i = 0; i < maxLateralIndex + 1; i++)
	{
		if (buttons[i].Num() - 1 > maxLateralIndex)
		{
			maxLateralIndex = buttons[i].Num() - 1;
		}
	}
};

UButton* UFourKeyNavigation::GetCurrentButton()
{
	return currentButton;
};

void UFourKeyNavigation::SetCurrentButton(UButton* newButton)
{
	currentButton = newButton;
};

void UFourKeyNavigation::Move(FKey input)
{
	// Even though a switch statement makes sense here, if statements are kept to simplify implementation
	// into Unreal Engine
	if (input == movementKeys[0])
	{
		MoveUp();
	}
	else if (input == movementKeys[1])
	{
		MoveDown();
	}
	else if (input == movementKeys[2])
	{
		MoveLeft();
	}
	else if (input == movementKeys[3])
	{
		MoveRight();
	}
};

void UFourKeyNavigation::MoveUp()
{
	if (verticalIndex > 0)
	{
		currentButton = buttons[lateralIndex][verticalIndex - 1];

		verticalIndex--;
	}
}

void UFourKeyNavigation::MoveDown()
{
	if (verticalIndex < maxVerticalIndex)
	{
		currentButton = buttons[lateralIndex][verticalIndex++];
		
		verticalIndex++;
	}
}

void UFourKeyNavigation::MoveLeft()
{
	if (lateralIndex > 0)
	{
		if (buttons[lateralIndex - 1].Num() < verticalIndex)
		{
			currentButton = buttons[lateralIndex - 1][buttons[lateralIndex - 1].Num() - 1];
		}
		else
		{
			currentButton = buttons[lateralIndex - 1][verticalIndex];
		}
		lateralIndex--;
	}
}

void UFourKeyNavigation::MoveRight()
{
	if (lateralIndex < maxLateralIndex)
	{
		if (buttons[lateralIndex + 1].Num() < verticalIndex)
		{
			currentButton = buttons[lateralIndex + 1][buttons[lateralIndex + 1].Num() - 1];
		}
		else
		{
			currentButton = buttons[lateralIndex + 1][verticalIndex];
		}
		lateralIndex++;
	}
}
