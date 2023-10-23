import {Form, Formik} from "formik";
import {Button, FormControl, InputLabel, MenuItem, Select, Stack, Switch, Tooltip, Typography} from "@mui/material";
import React, {useEffect, useState} from "react";
import {ShopWorkingDay, StationaryShop} from "../../../../../global/models/shop";
import {LocalizationProvider, TimeField} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import pl from "date-fns/locale/pl";
import {TimeRange} from "../../../../../global/models/common";
import {Label} from "@mui/icons-material";
import {toast} from "react-toastify";

interface Props {
    shop: StationaryShop
    onChange: (weekDay: string, openHours: TimeRange) => void
}

export function StationaryShopWeekDayUpdateModal({shop, onChange}: Props) {
    const init = {
        weekDay: '',
        openHours: {start: '', end: ''}
    }

    const weekDayTime = (values: {weekDay: string, openHours: {start: string, end: string}}) => {
        return [
            new Date(0,0,1,Number(values.openHours.start.split(':')[0]), Number(values.openHours.start.split(':')[1])),
            new Date(0,0,1,Number(values.openHours.end.split(':')[0]),Number(values.openHours.end.split(':')[1]))
        ]
    }

    const validateTime = (timeRange: TimeRange) => {
        const start = timeRange.start
        const end = timeRange.end
        const isValidTimeFormat = (time: string) => {
            return /(?:[0-1][0-9]|2[0-3]):[0-5][0-9]/.test(time);
        };
        const isHourValid = start.split(':')[0] <= end.split(':')[0]
        const isHoursEqual = start.split(':')[0] === end.split(':')[0]
        const isMinutesValid = isHoursEqual ? start.split(':')[1] < end.split(':')[1] : true
        return isValidTimeFormat(start) && isValidTimeFormat(end) && isHourValid && isMinutesValid
    }

    function handleChange(weekDay: string, openHours: TimeRange) {
        const throwToastIfInvalid = () => {
            if (openHours && !validateTime(openHours)) {
                toast.dismiss()
                toast.error(`Niewłaściwe godziny otwarcia`);
                return true;
            }
            toast.dismiss()
            return false;
        };

        if(throwToastIfInvalid()) {
            return
        }

        onChange(weekDay, openHours)
    }

    return (
        <Formik
            initialValues={init}
            onSubmit={(values) => handleChange(values.weekDay, values.openHours)}>
            {({handleSubmit, handleChange, isValid, values, setValues}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} style={{display: 'flex', justifyContent: 'center', alignItems: 'center'}} spacing={3}>
                        <Typography variant={'h4'}>{shop.shopName}</Typography>
                        {shop.weekSchedule.find(d => d.weekDay.value === values.weekDay)?.isClosed ?
                            <Tooltip title={'UWAGA - po zastosowaniu zmian sklep będzie otwarty w ten dzień'}
                                     arrow open={true} placement={'right'}>
                                <FormControl fullWidth required error={true}>
                                    <InputLabel>Dzień tygodnia</InputLabel>
                                    <Select
                                        name={'weekDay'}
                                        label="Dzień tygodnia"
                                        value={values.weekDay}
                                        onChange={(e) => {
                                            shop.weekSchedule.find(d => d.weekDay.value === e.target.value)?.openHours ?
                                                setValues({weekDay: e.target.value,
                                                    openHours: shop.weekSchedule.find(d => d.weekDay.value === e.target.value)!.openHours!})
                                                :
                                                setValues({weekDay: e.target.value,
                                                    openHours: {start: '', end: ''}})
                                        }}
                                    >
                                        {shop.weekSchedule.map((d, i) =>
                                            <MenuItem key={i} value={d.weekDay.value}>{d.weekDay.value}</MenuItem>
                                        )}
                                    </Select>
                                </FormControl>
                            </Tooltip>
                            :
                            <FormControl fullWidth required>
                                <InputLabel>Dzień tygodnia</InputLabel>
                                <Select
                                    name={'weekDay'}
                                    label="Dzień tygodnia"
                                    value={values.weekDay}
                                    onChange={(e) => {
                                        shop.weekSchedule.find(d => d.weekDay.value === e.target.value)?.openHours ?
                                            setValues({weekDay: e.target.value,
                                                openHours: shop.weekSchedule.find(d => d.weekDay.value === e.target.value)!.openHours!})
                                            :
                                            setValues({weekDay: e.target.value,
                                                openHours: {start: '', end: ''}})
                                    }}
                                >
                                    {shop.weekSchedule.map((d, i) =>
                                        <MenuItem key={i} value={d.weekDay.value}>{d.weekDay.value}</MenuItem>
                                    )}
                                </Select>
                            </FormControl>
                        }
                        {values.weekDay &&
                            <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={pl}>
                                <Stack direction={'column'} spacing={2}>
                                    <TimeField
                                        label="Otwarcie"
                                        name={'openHours.start'}
                                        size={'small'}
                                        value={weekDayTime(values)[0]}
                                        onChange={(date) => {
                                            setValues({
                                                ...values,
                                                openHours: {
                                                    end: values.openHours ? values.openHours.end : '',
                                                    start: (date as Date).toLocaleTimeString()
                                                }
                                            })
                                        }}
                                    />
                                    <TimeField
                                        label="Zamknięcie"
                                        name={'openHours.end'}
                                        size={'small'}
                                        value={weekDayTime(values)[1]}
                                        onChange={(date) => {
                                            setValues({
                                                ...values,
                                                openHours: {
                                                    start: values.openHours ? values.openHours.start : '',
                                                    end: (date as Date).toLocaleTimeString()
                                                }
                                            })
                                        }}
                                    />
                                </Stack>
                            </LocalizationProvider>
                        }
                        <Button disabled={values.weekDay.length < 1 || values.openHours.end.length < 1 || values.openHours.start.length < 1}
                                type={'submit'} onClick={() => handleSubmit} variant={'contained'}>
                            <Typography>Zaktualizuj</Typography>
                        </Button>
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}