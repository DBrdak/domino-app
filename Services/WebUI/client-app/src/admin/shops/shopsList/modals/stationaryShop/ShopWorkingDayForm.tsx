import {Form, Formik} from "formik";
import {
    Button,
    Card,
    CardActions,
    CardHeader,
    FormControl,
    InputLabel,
    MenuItem,
    Select,
    Stack, Switch,
    Typography
} from "@mui/material";
import React, {useEffect, useState} from "react";
import {ShopWorkingDay} from "../../../../../global/models/shop";
import {LocalizationProvider, TimeField} from "@mui/x-date-pickers";
import {AdapterDateFns} from "@mui/x-date-pickers/AdapterDateFns";
import pl from "date-fns/locale/pl";
import {isCancel} from "axios";
import {values} from "mobx";
import * as yup from "yup";
import {TimeRange} from "../../../../../global/models/common";
import {toast} from "react-toastify";

interface Props {
    weekDay: ShopWorkingDay
    onChange: (workingDay: ShopWorkingDay | string) => void
}

export function ShopWorkingDayForm({onChange, weekDay}: Props) {

    function handleChange(workingDay: ShopWorkingDay) {
        const isValidTimeFormat = (time: string) => {
            return /^(?:[0-1][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$/.test(time);
        };

        const throwToastIfInvalid = () => {
            if(!workingDay.openHours && !workingDay.isClosed) {
                return true
            }

            if (workingDay.openHours && (!isValidTimeFormat(workingDay.openHours.start) || !isValidTimeFormat(workingDay.openHours.end))) {
                return true;
            }

            if (workingDay.openHours && !validateTime(workingDay.openHours)) {
                const dayOfWeek = workingDay.weekDay.value === 'Wtorek' ? 'we' : 'w';
                toast.error(`Niewłaściwe godziny otwarcia ${dayOfWeek} ${workingDay.weekDay.value}`);
                return true;
            }
            toast.dismiss()
            return false;
        };

        if(throwToastIfInvalid()) {
            onChange(weekDay.weekDay.value)
            return
        }

        if(workingDay.isClosed && workingDay.openHours){
            workingDay.openHours = null
        }

        onChange(workingDay)
    }

    const validateTime = (timeRange: TimeRange) => {
        const start = timeRange.start
        const end = timeRange.end
        const isHourValid = start.split(':')[0] <= end.split(':')[0]
        const isHoursEqual = start.split(':')[0] === end.split(':')[0]
        const isMinutesValid = isHoursEqual ? start.split(':')[1] < end.split(':')[1] : true

        return isHourValid && isMinutesValid
    }

    return (
        <Formik
            initialValues={weekDay}
            onSubmit={(values) => handleChange(values)}>
            {({handleSubmit, handleChange, isValid, values, setValues}) => (
                <Form style={{width: '100%'}} onSubmit={handleSubmit} autoComplete='off'>
                    <Stack direction={'column'} spacing={2}
                            style={{
                                display: 'flex',
                                justifyContent: 'center',
                                alignItems: 'center',
                                padding: '8px',
                                border: '1px solid #BBBBBB',
                                borderRadius: '10px',
                                boxShadow: '0px 0px 10px 0px #BBBBBB',
                                minWidth: '150px'
                            }}>
                        <Typography variant={'h6'}>{values.weekDay.value}</Typography>
                        <Switch
                            name={'isClosed'}
                            onChange={() => {
                                setValues({...values, isClosed: !values.isClosed, openHours: values.isClosed ? null : values.openHours })
                                handleSubmit()
                            }}
                            value={!values.isClosed}
                        />
                        {!values.isClosed &&
                            <LocalizationProvider dateAdapter={AdapterDateFns} adapterLocale={pl}>
                                <Stack direction={'column'} spacing={2}>
                                    <TimeField
                                        label="Otwarcie"
                                        name={'openHours.start'}
                                        size={'small'}
                                        onChange={(date) => {
                                            setValues({
                                                ...values,
                                                openHours: {
                                                    end: values.openHours ? values.openHours.end : '',
                                                    start: (date as Date) ? (date as Date).toLocaleTimeString() : ''
                                                }
                                            })
                                            handleSubmit()
                                        }}
                                    />
                                    <TimeField
                                        label="Zamknięcie"
                                        name={'openHours.end'}
                                        size={'small'}
                                        onChange={(date) => {
                                            setValues({
                                                ...values,
                                                openHours: {
                                                    start: values.openHours ? values.openHours.start : '',
                                                    end: (date as Date) ? (date as Date).toLocaleTimeString() : ''
                                                }
                                            })
                                            handleSubmit()
                                        }}
                                    />
                                </Stack>
                            </LocalizationProvider
                        >}
                    </Stack>
                </Form>
            )}
        </Formik>
    );
}