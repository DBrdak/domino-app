import {ShopWorkingDay, StationaryShop} from "../../../../../global/models/shop";
import {Button, Stack, Typography} from "@mui/material";
import React, {useState} from "react";
import {ShopWorkingDayForm} from "./ShopWorkingDayForm";

interface Props {
    shop: StationaryShop
    onSubmit: (schedule: ShopWorkingDay[]) => void
}

export function StationaryShopWorkScheduleInitModal({shop, onSubmit}: Props) {
    const [validators, setValidators] = useState<{id: String, isValid: boolean }[]>([
        {id: 'Poniedziałek', isValid: true},
        {id: 'Wtorek', isValid: true},
        {id: 'Środa', isValid: true},
        {id: 'Czwartek', isValid: true},
        {id: 'Piątek', isValid: true},
        {id: 'Sobota', isValid: true},
        {id: 'Niedziela', isValid: true},
    ])

    const [workingDays, setWorkingDays] = useState<ShopWorkingDay[]>([
        {
            weekDay: {value: 'Poniedziałek'},
            openHours: null,
            isClosed: true,
            cachedOpenHours: null
        },
        {
            weekDay: {value: 'Wtorek'},
            openHours: null,
            isClosed: true,
            cachedOpenHours: null
        },
        {
            weekDay: {value: 'Środa'},
            openHours: null,
            isClosed: true,
            cachedOpenHours: null
        },
        {
            weekDay: {value: 'Czwartek'},
            openHours: null,
            isClosed: true,
            cachedOpenHours: null
        },
        {
            weekDay: {value: 'Piątek'},
            openHours: null,
            isClosed: true,
            cachedOpenHours: null
        },
        {
            weekDay: {value: 'Sobota'},
            openHours: null,
            isClosed: true,
            cachedOpenHours: null
        },
        {
            weekDay: {value: 'Niedziela'},
            openHours: null,
            isClosed: true,
            cachedOpenHours: null
        }
    ])

    const handleChange = (workingDay: ShopWorkingDay | string) => {
        if(typeof workingDay === typeof('')) {
            const workingDayParsed = workingDay as String
            setValidators(prev => {
                return prev.map(v => {return {id: v.id, isValid: v.id === workingDayParsed ? false : v.isValid}})
            })
        } else {
            const workingDayParsed = workingDay as ShopWorkingDay
            setValidators(prev => {
                return prev.map(v => {return {id: v.id, isValid: v.id === workingDayParsed.weekDay.value ? true : v.isValid}})
            })
            setWorkingDays(prev => {
                return prev.map(d => d.weekDay === workingDayParsed.weekDay ? workingDayParsed : d)
            })
            console.log(validators)
            console.log(workingDays)
        }
    }

    const handleSubmit = () => {
        console.log(workingDays)
        onSubmit(workingDays)
    }

    return (
        <Stack direction={'column'} spacing={4} style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
            <Typography variant={'h4'}>Tydzień pracy</Typography>
            <Stack direction={'row'} spacing={2}
                   style={{display: 'flex', justifyContent: 'center', alignItems: 'center', height: '250px'}}>
                {workingDays.map(d => (
                    <ShopWorkingDayForm key={d.weekDay.value} weekDay={d}
                                        onChange={(workingDay) => handleChange(workingDay)} />
                ))}
            </Stack>
            <Button disabled={!(validators.find(v => !v.isValid) !== null) || workingDays.filter(d => d.isClosed).length >= 7}
                    style={{width: '30%'}} onClick={() => handleSubmit()} variant={'contained'}>
                <Typography>Zaakceptuj</Typography>
            </Button>
        </Stack>
    )
}