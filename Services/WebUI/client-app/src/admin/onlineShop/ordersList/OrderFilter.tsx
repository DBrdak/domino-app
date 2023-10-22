import {Button, FormControl, InputLabel, MenuItem, Popover, Select, Typography} from "@mui/material";
import {FilterAlt} from "@mui/icons-material";
import React, {useState} from "react";
import {Shop} from "../../../global/models/shop";

interface Props {
    shops: Shop[]
    handleShopChange: (shopName: string | null) => void
}

export function OrderFilter({shops, handleShopChange}:Props) {

    return (
        <FormControl fullWidth>
            <InputLabel>Sklep</InputLabel>
            <Select
                name={'shop'}
                label="Sklep"
                onChange={(e => {
                    if(e.target.value as string === ''){
                        handleShopChange(null)
                        return
                    }
                    handleShopChange(e.target.value as string)
                })}
            >
                <MenuItem value={''}>Wyczyść</MenuItem>
                {shops.map(s => (
                    <MenuItem key={s.id} value={s.shopName}>{s.shopName}</MenuItem>
                ))}
            </Select>
        </FormControl>
    );
}