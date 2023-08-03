import React, { useState } from 'react';
import {Grid,TextField,Checkbox,FormControlLabel,FormControl,InputLabel,Select,MenuItem,useMediaQuery,useTheme, Button, Paper, IconButton, Stack, Typography} from '@mui/material';
import { FilterList, Sort } from '@mui/icons-material';
import RevealButton from '../components/RevealButton';

interface FilterPanelProps {
  onApplyFilter: (filterOptions: FilterOptions) => void;
  onApplySearch: (productName: string | null) => void;
  subcategories: string[]
}

const FilterPanel: React.FC<FilterPanelProps> = ({ onApplyFilter, onApplySearch, subcategories }) => {
  const [filterOptions, setFilterOptions] = useState<FilterOptions>({
    subcategory: '',
    minPrice: null,
    maxPrice: null,
    isAvailable: true,
    isDiscounted: false,
    sortProperty: 'name',
    sortDirection: 'asc',
    pcsMode: false,
  });
  const [searchPhrase, setSearchPhrase] = useState<string | null>(null)

  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));

  const handleApplyFilter = () => {
    onApplyFilter(filterOptions);
  };

  const handleApplySearch = (productName: string | null) => {
    setSearchPhrase(productName)
    onApplySearch(productName);
  };

  function handleSortDirChange(): void {
    filterOptions.sortDirection === 'asc' ? 
      setFilterOptions({...filterOptions, sortDirection: 'desc'})
      : setFilterOptions({...filterOptions, sortDirection: 'asc'})
  }

  return (
    (isMobile ?
      <RevealButton buttonText='Filtrowanie' revealComponent={
          <Paper style={{padding: '40px'}}>
          <Typography variant='h5' paddingBottom={'20px'} textAlign={'center'}>Wyszukiwanie</Typography>
            <TextField
              style={{marginBottom: '15px'}}
              fullWidth
              label="Nazwa produktu"
              onChange={(e) => handleApplySearch(e.target.value)}
            />
            <Button style={{marginBottom: '25px'}} variant='contained' onClick={() => handleApplySearch(searchPhrase)}>Szukaj</Button>
            <Typography variant='h5' paddingBottom={'20px'} textAlign={'center'}>Filtry</Typography>
            <Grid container spacing={2}>
              <Grid item xs={12}>
              <FormControl fullWidth>
                <InputLabel>Podkategoria</InputLabel>
                <Select
                  value={filterOptions.subcategory}
                  onChange={(e) => setFilterOptions({...filterOptions, subcategory: String(e.target.value)})}
                >
                  {subcategories.map(s => 
                    s !== '' && <MenuItem key={s} value={s}>{s}</MenuItem>
                  )}
                </Select>
                {filterOptions.subcategory !== '' && (
                  <Button variant="outlined" onClick={(e) => setFilterOptions({...filterOptions, subcategory: ''})}>
                    Wyczyść
                  </Button>
                )}
              </FormControl>
                <TextField
                  margin='normal'
                  fullWidth
                  label="Minimalna cena"
                  type="number"
                  value={filterOptions.minPrice || ''}
                  onChange={(e) =>
                    setFilterOptions({ ...filterOptions, minPrice: Number(e.target.value) || null })
                  }
                />
                <TextField
                  style={{textAlign: 'center'}}
                  fullWidth
                  label="Maksymalna cena"
                  type="number"
                  value={filterOptions.maxPrice || ''}
                  onChange={(e) =>
                    setFilterOptions({ ...filterOptions, maxPrice: Number(e.target.value) || null })
                  }
                />
          </Grid>
          <Grid item xs={12}>
            <FormControlLabel
              control={
                <Checkbox
                  checked={filterOptions.isAvailable}
                  onChange={(e) => setFilterOptions({ ...filterOptions, isAvailable: e.target.checked })}
                />
              }
              label="Dostępny"
            />
            <FormControlLabel
              control={
                <Checkbox
                  checked={filterOptions.isDiscounted}
                  onChange={(e) => setFilterOptions({ ...filterOptions, isDiscounted: e.target.checked })}
                />
              }
              label="Promocja"
            />
            <FormControlLabel
              control={
                <Checkbox
                  checked={filterOptions.pcsMode}
                  onChange={(e) => setFilterOptions({ ...filterOptions, pcsMode: e.target.checked })}
                />
              }
              label="Pokaż cenę za sztukę"
            />
          </Grid>
          <Stack direction={'row'} margin={'10px 0px 10px 0px'} 
          style={{width: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
            <FormControl fullWidth>
              <InputLabel>Sortuj wg</InputLabel>
              <Select
                value={filterOptions.sortProperty}
                onChange={(e) =>
                  setFilterOptions({ ...filterOptions, sortProperty: e.target.value as string })
                }
              >
                <MenuItem value="name">Nazwa produktu</MenuItem>
                <MenuItem value="price">Cena</MenuItem>
              </Select>
            </FormControl>
            <IconButton onClick={() => handleSortDirChange()}>
            {filterOptions.sortDirection === 'asc' ? 
            <FilterList style = {{transform: 'rotate(180deg)'}} fontSize='medium'/> :
            <FilterList fontSize='medium'/>}
          </IconButton>
          </Stack>
          <Grid item xs={12} textAlign={'center'}>
            <Button variant='contained' onClick={handleApplyFilter}>Filtruj</Button>
          </Grid>
        </Grid>
      </Paper>
      } />

    :
    <Paper style={{padding: '40px'}}>          
      <Typography variant='h5' paddingBottom={'20px'} textAlign={'center'}>Wyszukiwanie</Typography>
      <TextField
        style={{marginBottom: '25px'}}
        fullWidth
        label="Nazwa produktu"
        onChange={(e) => handleApplySearch(e.target.value)}
      />
      <Typography variant='h5' paddingBottom={'20px'} textAlign={'center'}>Filtry</Typography>
      <Grid container spacing={2}>
        <Grid item xs={12}>
        <FormControl fullWidth>
          <InputLabel>Podgategoria</InputLabel>
          <Select
            value={filterOptions.subcategory}
            onChange={(e) => setFilterOptions({...filterOptions, subcategory: String(e.target.value)})}
          >
            {subcategories.map(s => 
              s !== '' && <MenuItem key={s} value={s}>{s}</MenuItem>
            )}
          </Select>
          {filterOptions.subcategory !== '' && (
            <Button variant="outlined" onClick={(e) => setFilterOptions({...filterOptions, subcategory: ''})}>
              Wyczyść
            </Button>
          )}
        </FormControl>
          <TextField
            margin='normal'
            label="Minimalna cena"
            type="number"
            value={filterOptions.minPrice || ''}
            onChange={(e) =>
              setFilterOptions({ ...filterOptions, minPrice: Number(e.target.value) || null })
            }
          />
          <TextField
            label="Maksymalna cena"
            type="number"
            value={filterOptions.maxPrice || ''}
            onChange={(e) =>
              setFilterOptions({ ...filterOptions, maxPrice: Number(e.target.value) || null })
            }
          />
        </Grid>
        <Grid item xs={12}>
          <FormControlLabel
            control={
              <Checkbox
                checked={filterOptions.isAvailable}
                onChange={(e) => setFilterOptions({ ...filterOptions, isAvailable: e.target.checked })}
              />
            }
            label="Dostępny"
          />
          <FormControlLabel
            control={
              <Checkbox
                checked={filterOptions.isDiscounted}
                onChange={(e) => setFilterOptions({ ...filterOptions, isDiscounted: e.target.checked })}
              />
            }
            label="Promocja"
          />
          <FormControlLabel
              control={
                <Checkbox
                  checked={filterOptions.pcsMode}
                  onChange={(e) => setFilterOptions({ ...filterOptions, pcsMode: e.target.checked })}
                />
              }
              label="Pokaż cenę za sztukę"
              style={{whiteSpace: 'nowrap'}}
            />
        </Grid>
        <Stack direction={'row'} margin={'10px 0px 10px 0px'} 
        style={{width: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
          <FormControl fullWidth>
            <InputLabel>Sortuj wg</InputLabel>
            <Select
              value={filterOptions.sortProperty}
              onChange={(e) =>
                setFilterOptions({ ...filterOptions, sortProperty: e.target.value as string })
              }
            >
              <MenuItem value="name">Nazwa produktu</MenuItem>
              <MenuItem value="price">Cena</MenuItem>
            </Select>
          </FormControl>
          <IconButton onClick={() => handleSortDirChange()}>
          {filterOptions.sortDirection === 'asc' ? 
          <FilterList style = {{transform: 'rotate(180deg)'}} fontSize='medium'/> :
          <FilterList fontSize='medium'/>}
        </IconButton>
        </Stack>
        <Grid item xs={12} textAlign={'center'}>
          <Button variant='contained' onClick={handleApplyFilter}>Filtruj</Button>
        </Grid>
      </Grid>
    </Paper>)
  );
};

export default FilterPanel;
