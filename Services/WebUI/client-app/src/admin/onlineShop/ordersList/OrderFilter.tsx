import {Button, FormControl, IconButton, InputLabel, MenuItem, Popover, Select, Stack, Typography} from "@mui/material";
import {Clear, FilterAlt} from "@mui/icons-material";
import React, {useState} from "react";
import {Shop} from "../../../global/models/shop";
import {setSelectionRange} from "@testing-library/user-event/dist/utils";

interface Props {
    shops: Shop[]
    selectedName: string
    handleShopChange: (shopName: string) => void
}

export function OrderFilter({shops, handleShopChange, selectedName}:Props) {

    return (
        <Stack direction={'row'}>
            <FormControl fullWidth>
                <InputLabel>Sklep</InputLabel>
                <Select
                    value={selectedName}
                    label="Sklep"
                    onChange={(e => {
                        handleShopChange(e.target.value as string)
                    })}
                >
                    {shops.map(s => (
                        <MenuItem key={s.id} value={s.shopName}>{s.shopName}</MenuItem>
                    ))}
                </Select>
            </FormControl>
            <Button style={{width: '20%', borderRadius: '0px', border: '0.7px solid #c4c4c4', color: '#8f8f8f'}}
                    onClick={() => handleShopChange('')}>
                <Clear fontSize={'large'} />
            </Button>
        </Stack>
    );
}