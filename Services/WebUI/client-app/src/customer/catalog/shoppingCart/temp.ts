export function getNextDay(targetDayName: string, targetHour = 0, targetMinute = 0, targetSecond = 0):Date {
  const dayNames = ["Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela"];
  const currentDay = new Date();
  const currentDayIndex = currentDay.getDay();
  const targetDayIndex = dayNames.indexOf(targetDayName);

  if (targetDayIndex === -1) {
      throw new Error("Invalid day name");
  }

  // Calculate the difference between the current day and the target day
  let dayDifference = targetDayIndex - currentDayIndex;
  if (dayDifference <= 0) {
      dayDifference += 7; // If the target day has already occurred this week, get the next week's occurrence
  }

  // Set the target date and time
  const targetDate = new Date(currentDay);
  targetDate.setDate(currentDay.getDate() + dayDifference);
  targetDate.setHours(targetHour, targetMinute, targetSecond, 0); // Set hours, minutes, seconds, and milliseconds

  return targetDate;
}

function capitalizeFirstLetter(string: string) {
  return string.charAt(0).toUpperCase() + string.slice(1);
}

export function getPolishDayOfWeek(date: Date) {
  const dayName = date.toLocaleDateString('pl-PL', { weekday: 'long' });
  return capitalizeFirstLetter(dayName);
}