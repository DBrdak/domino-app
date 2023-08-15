import React from 'react'
import { DateTimeRange } from '../global/models/common'

interface Props {
  date: DateTimeRange
}

function DateTimeRangeDisplay({date}:Props) {
  
  function capitalizeFirstLetter(string: string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
  }

  const start ={
    dayOfWeek: new Date(date.start).toLocaleDateString('pl-PL', { weekday: 'long' }),
    dayAndMonth: new Date(date.start).toLocaleDateString('pl-PL', { day: '2-digit', month: '2-digit' }),
    time: new Date(date.start).toLocaleTimeString('pl-PL', { hour: '2-digit', minute: '2-digit' })
  }
  const end ={
    dayOfWeek: new Date(date.end).toLocaleDateString('pl-PL', { weekday: 'long' }),
    dayAndMonth: new Date(date.end).toLocaleDateString('pl-PL', { day: '2-digit', month: '2-digit' }),
    time: new Date(date.end).toLocaleTimeString('pl-PL', { hour: '2-digit', minute: '2-digit' })
  }

  let formattedStart = ''
  let formattedEnd = ''

  if(start.dayAndMonth === end.dayAndMonth && start.dayOfWeek === end.dayOfWeek) {
    formattedStart = `${capitalizeFirstLetter(start.dayOfWeek)} ${start.dayAndMonth} godz. ${start.time}`
    formattedEnd = `${end.time}`
  } else {
    formattedStart = `${start.dayOfWeek} ${start.dayAndMonth} godz. ${start.time}`
    formattedEnd = `${end.dayOfWeek} ${end.dayAndMonth} godz. ${end.time}`
  }

  return (
    <div>
      {formattedStart} - {formattedEnd}
    </div>
  )
}

export default DateTimeRangeDisplay